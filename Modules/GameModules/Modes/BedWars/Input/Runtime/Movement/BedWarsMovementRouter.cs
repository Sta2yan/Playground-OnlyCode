using Agava.AdditionalPredefinedMethods;
using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Movement;
using UnityEngine;

namespace Agava.Playground3D.Input
{
    internal class BedWarsMovementRouter : IGameLoop, IGameFixedLoop, IMovementRouter
    {
        private readonly IInput _input;
        private readonly IDisplacementObject _displacementObject;
        private readonly CameraMovement _camera;
        private readonly bool _mobileInput;

        private bool _enabled = true;
        private Vector2 _moveDirection;

        public IInput Input => _input;
        
        public BedWarsMovementRouter(IInput input, IDisplacementObject displacementObject, CameraMovement camera, bool mobileInput)
        {
            _input = input;
            _displacementObject = displacementObject;
            _camera = camera;

            _mobileInput = mobileInput;
        }

        public void Update(float _)
        {
            if (_enabled == false)
            {
                _displacementObject.TryMove(0, 0, _camera.FirstPersonPerspective);
                return;
            }
            
            _moveDirection = _input.MoveDirection();
            var rotateDirection = Vector3.zero;
            
            if (_input.RotateDirection(out var direction) || _camera.FirstPersonPerspective && _mobileInput)
                rotateDirection = direction;

            if (_input.Jump())
                _displacementObject.TryJump();
            
            if (_input.Sprint())
                _displacementObject.TryEnableSprint(_moveDirection.x, _moveDirection.y);
            else
                _displacementObject.DisableSprint();
            
            _camera.ChangeRotation(rotateDirection.x, rotateDirection.y);
            _camera.ChangeDistance(_input.ChangeCameraDistance());
        }

        public void FixedUpdate()
        {
            _displacementObject.TryMove(_moveDirection.x, _moveDirection.y, _camera.FirstPersonPerspective);
        }
        
        public void SetActive(bool value)
        {
            _enabled = value;
        }
    }
}
