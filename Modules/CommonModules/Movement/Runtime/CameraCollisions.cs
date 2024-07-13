using UnityEngine;

namespace Agava.Movement
{
    internal class CameraCollisions : MonoBehaviour
    {
        private const float OffsetFault = .05f;
        
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private LayerMask _collisions;

        private void LateUpdate()
        {
            ChangePositionBy(_collisions);
        }

        private void ChangePositionBy(LayerMask collisions)
        {
            if (Physics.Linecast(transform.position, _cameraMovement.CameraMain.transform.position, out var cameraCollisionHit, collisions))
                _cameraMovement.CameraMain.transform.position = cameraCollisionHit.point + cameraCollisionHit.normal * OffsetFault;
        }
    }
}
