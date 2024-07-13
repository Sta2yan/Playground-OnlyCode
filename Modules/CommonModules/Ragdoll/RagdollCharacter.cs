using Agava.Combat;
using System.Collections;
using UnityEngine;

namespace Agava.Ragdoll
{
    public class RagdollCharacter : MonoBehaviour, IRagdollCharacter
    {
        [SerializeField] private Rigidbody _mainRigidbody;
        [SerializeField] private Collider _mainCollider;
        [SerializeField] private RagdollPart[] _ragdollParts;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _root;
        [SerializeField] private RagdollPart _mainRagdollPart;

        private Sword _sword;
        private Coroutine _coroutine;

        public bool RagdollEnabled { get; private set; } = false;

        private void Awake()
        {
            foreach (RagdollPart ragdollPart in _ragdollParts)
            {
                ragdollPart.Initialize(this);
            }

            Disable();
        }

        [ContextMenu("EnableRagdoll")]
        public void Enable(int attackLayers = 0)
        {
            RagdollEnabled = true;
            ChangeMode();
            if(attackLayers != 0)
            {
                if (_coroutine == null)
                    _coroutine = StartCoroutine(FindSword());

            }

            IEnumerator FindSword()
            {
                WaitForSeconds delay = new WaitForSeconds(0.5f);

                while(_sword == null)
                {
                    _sword = gameObject.GetComponentInChildren<Sword>();

                    yield return delay;
                }
                _sword.ChangeAttackLayer(attackLayers);
            }
        }

        [ContextMenu("DisableRagdoll")]
        public void Disable()
        {
            RagdollEnabled = false;
            ChangeMode();
            if(_sword != null)
            {
                _sword.ChangeAttackLayer(0);
                _sword = null;
            }

            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            
        }

        private void ChangeMode()
        {
            _mainRigidbody.useGravity = !RagdollEnabled;
            _mainRigidbody.isKinematic = RagdollEnabled;

            foreach (RagdollPart ragdollPart in _ragdollParts)
            {
                if (RagdollEnabled)
                {
                    ragdollPart.Enable();
                }
                else
                {
                    ragdollPart.Disable();
                }
            }

            _animator.enabled = !RagdollEnabled;
        }

        public void FreezePosition(RagdollPart pivot)
        {
            if (pivot != null)
            {
                if (pivot.RagdollCharacter == this)
                {
                    pivot.FreezePosition();
                }
            }
        }

        public bool TryGetNearestRagdollPart(Vector3 position, out RagdollPart targetRagdollPart)
        {
            //targetRagdollPart = null;
            //float minDistance = float.MaxValue;
            //float distance;

            //foreach (RagdollPart ragdollPart in _ragdollParts)
            //{
            //    distance = Vector3.Distance(ragdollPart.WorldCenterOfMass, position);

            //    if (distance < minDistance)
            //    {
            //        minDistance = distance;
            //        targetRagdollPart = ragdollPart;
            //    }
            //}

            targetRagdollPart = _mainRagdollPart;

            return targetRagdollPart != null;
        }
    }
}