using Agava.AdditionalPredefinedMethods;
using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Input;
using Agava.Ragdoll;
using UnityEngine;
using Agava.Combat;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.GravyGun
{
    public class RagdollRouter : IGameLoop
    {
        private const float MaxDistance = 20;

        private readonly IInput _input;
        private readonly Hand _hand;
        private readonly GameObject _pointer;
        private readonly LayerMask _charactersMask;
        private readonly LayerMask _collisionsMask;
        private readonly CameraMovement _cameraMovement;
        private readonly LayerMask _attackLayers;

        private bool _mobile;
        private JointSetter _pivot;
        private RagdollCharacter _currentCharacter;
        private Vector3 _pivotPosition;
        private float _distance;
        private Vector3 _initialInputPosition;
        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _gravityGunUseEventRule;

        private bool _holdingCharacter => _currentCharacter != null;
        private Camera _camera => _cameraMovement.CameraMain;

        public RagdollRouter(IInput input, Hand hand, GameObject pointer,
            LayerMask charactersMask, LayerMask collisionsMask,
            CameraMovement cameraMovement, bool mobile,
            ExperienceEventsContainer experienceEventsContainer,
            ExperienceEventRule gravityGunUseEventRule,
            GameObject pivot,
            LayerMask attackLayers)
        {
            _input = input;
            _hand = hand;
            _pointer = pointer;
            _charactersMask = charactersMask;
            _collisionsMask = collisionsMask;
            _cameraMovement = cameraMovement;
            _mobile = mobile;
            _experienceEventsContainer = experienceEventsContainer;
            _gravityGunUseEventRule = gravityGunUseEventRule;
            _attackLayers = attackLayers;
            _pivot = pivot.GetComponent<JointSetter>();

            DisablePointer();
        }

        public void Update(float _)
        {
            GameObject handItem = _hand.ItemInstance;

            if (handItem == null)
            {
                DisableBeam();
                return;
            }

            if (handItem.TryGetComponent(out GravityGun gravityGun))
            {
                if (_input.UseItem())
                {
                    if (_holdingCharacter)
                    {
                        DisableBeam();
                    }
                    else
                    {
                        TryEnableBeam();
                    }
                }
                else
                {
                    if (_holdingCharacter)
                    {
                        if (_currentCharacter.TryGetComponent(out CombatCharacter combatCharacter))
                        {
                            if (combatCharacter.Alive == false)
                            {
                                DisableBeam();
                                return;
                            }
                        }

                        MoveCharacter(gravityGun);
                    }
                }
            }
            else
            {
                DisableBeam();
            }

            if (handItem.TryGetComponent(out RagdollManipulator ragdollManipulator))
            {
                if (_input.UseItem())
                    ragdollManipulator.Use();
            }
        }

        public void DisableBeam()
        {
            if (_holdingCharacter)
            {
                _currentCharacter.Disable();
                _currentCharacter.transform.parent = _pivot.transform.parent;
                _currentCharacter = null;

                _experienceEventsContainer.TriggerEvent(_gravityGunUseEventRule.ExperienceEvent());
            }

            DisablePointer();
        }

        private bool TryEnableBeam()
        {
            _initialInputPosition = ScreenPosition(true);
            Ray ray = RayBeamDirection(_initialInputPosition);

            if (TryGetTargetPart(ray, out RagdollPart ragdolPart, out Vector3 hitPosition))
            {
                _currentCharacter = ragdolPart.GetComponentInParent<RagdollCharacter>();
                _pivot.transform.SetParent(_currentCharacter.transform.parent);
                _currentCharacter.Enable(_attackLayers.value);
                _pivotPosition = hitPosition;

                if (_currentCharacter.TryGetNearestRagdollPart(hitPosition, out RagdollPart ragdollPart))
                {
                    _currentCharacter.FreezePosition(ragdollPart);
                }
                else
                {
                    return false;
                }

                _pivot.transform.position = _pivotPosition;
                _currentCharacter.transform.parent = _pivot.transform;
                _pivot.SetConnectedBod(ragdolPart.GetComponent<Rigidbody>());

                _distance = Vector3.Dot(
                     _pivotPosition - _camera.transform.position,
                     _camera.transform.forward
                   );

                EnablePointer();
                return true;
            }

            bool TryGetTargetPart(Ray ray, out RagdollPart ragdolPart, out Vector3 hitPosition)
            {
                ragdolPart = null;
                hitPosition = Vector3.zero;

                if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, _charactersMask))
                {
                    hitPosition = hit.point;
                    return hit.collider.TryGetComponent(out ragdolPart);
                }

                return false;
            }
            return false;
        }

            private bool TryGetTargetCharacter(Ray ray, out RagdollCharacter ragdollCharacter, out Vector3 hitPosition)
        {
            ragdollCharacter = null;
            hitPosition = Vector3.zero;

            if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, _charactersMask))
            {
                hitPosition = hit.point;
                return hit.collider.TryGetComponent(out ragdollCharacter);
            }

            return false;
        }

        private void MoveCharacter(GravityGun gravityGun)
        {
            Vector3 screenPosition = _mobile ? _initialInputPosition : ScreenPosition(false);
            screenPosition.z = _distance;

            Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);

            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit info, MaxDistance, _collisionsMask, QueryTriggerInteraction.Ignore))
            {
                Vector3 hitPoint = info.point;

                if (Vector3.Dot(hitPoint - _camera.transform.position, _camera.transform.forward) < _distance)
                {
                    Vector3 normal = info.normal;
                    worldPosition = hitPoint + normal.normalized;
                }
            }

            //_pivotPosition = Vector3.Lerp(_pivotPosition, worldPosition, gravityGun.MovementSpeed * Time.deltaTime);

            _pivotPosition = worldPosition;

            _pointer.transform.position = _pivotPosition;
            _pivot.transform.position = _pivotPosition;
        }

        private Ray RayBeamDirection(Vector3 input)
        {
            return _camera.ScreenPointToRay(input);
        }

        private Vector2 ScreenPosition(bool touch)
        {
            return _cameraMovement.FirstPersonPerspective ? _input.FirstPersonInput(touch) : _input.ThirdPersonInput(touch);
        }

        private void EnablePointer()
        {
#if UNITY_EDITOR
            _pointer.SetActive(true);
#endif
        }

        private void DisablePointer()
        {
            _pointer.SetActive(false);
        }
    }
}
