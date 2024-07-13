using UnityEngine;

namespace Agava.Movement
{
    public class PlayerMove : Move
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        protected override void GetDirection(float horizontal, float vertical, out Vector3 rawDirection, out Vector3 direction)
        {
            rawDirection = new Vector3(vertical, 0, -horizontal).normalized;

            if (rawDirection != Vector3.zero)
                direction = Quaternion.Euler(0f, _mainCamera.transform.rotation.eulerAngles.y - 90f, 0f) * rawDirection;
            else
                direction = new Vector3(_mainCamera.transform.forward.x, 0f, _mainCamera.transform.forward.z);
        }
    }
}
