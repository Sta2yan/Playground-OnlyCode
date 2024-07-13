using UnityEngine;

namespace Agava.Blocks.Sample
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControllerTest : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _lookSpeed = 3f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _gravity = -9.81f;

        private CharacterController _controller;
        private Vector3 _moveDirection;
        private float _rotationX = 0f;
        private float _rotationY = 0f;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = transform.right * horizontal + transform.forward * vertical;
            _controller.Move(movement * _moveSpeed * Time.deltaTime);

            if (Input.GetMouseButton(1))
            {
                var mouseX = Input.GetAxis("Mouse X") * _lookSpeed;
                var mouseY = Input.GetAxis("Mouse Y") * _lookSpeed;
                _rotationX -= mouseY;
                _rotationY += mouseX;
                _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
                transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
                
            }
            
            if (_controller.isGrounded)
            {
                _moveDirection.y = 0f;

                if (Input.GetButtonDown("Jump"))
                    _moveDirection.y = _jumpForce;
            }

            _moveDirection.y += _gravity * Time.deltaTime;
            _controller.Move(_moveDirection * Time.deltaTime);
        }
    }
}
