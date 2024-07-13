using System.Linq;
using Agava.CheckPoints;
using Agava.Combat;
using Agava.Movement;
using UnityEngine;

namespace Agava.Playground3D.RespawnLogic
{
    public class RespawnOnCheckPoints : MonoBehaviour
    {
        [SerializeField] private CombatCharacter _combatCharacter;
        [SerializeField] private PointsContainer _pointsContainer;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private CameraMovement _cameraRoot;
        [SerializeField] private int _advertisementCooldown;

        private float _lastAdvertisementTime = 0;
        private Advertisement.Advertisement _advertisement;

        private void Update()
        {
            float time = Time.timeSinceLevelLoad;

            if (_combatCharacter.Alive == false)
            {
                if (_lastAdvertisementTime + _advertisementCooldown <= time)
                {
                    
                    _lastAdvertisementTime = time;
                }

                _combatCharacter.Revive();

                if (_pointsContainer.CanDisplaceObjectToPoint)
                {
                    _pointsContainer.DisplaceToLastCompletePoint(_combatCharacter.transform, Vector3.up);

                    if (_cameraRoot != null)
                    {
                        _cameraRoot.ChangeTargetRotation(_pointsContainer.Points.Last(point => point.Complete).CameraRotate);
                    }
                }
                else
                {
                    _combatCharacter.Move(_startPoint.position);

                    if (_cameraRoot != null)
                    {
                        _cameraRoot.ChangeTargetRotation(Vector3.zero);
                    }
                }
            }
        }

        public void Initialize(Advertisement.Advertisement advertisement)
        {
            _advertisement = advertisement;
        }
    }
}
