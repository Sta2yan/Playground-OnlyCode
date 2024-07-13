using Agava.AdditionalPredefinedMethods;
using Agava.Input;
using Agava.Movement;
using UnityEngine;

namespace Agava.Playground3D.Movement
{
    public class OnlyUpMovementRouter : IGameLoop, IGameFixedLoop, IMovementRouter
    {
        private readonly IInput _input;
        private readonly IDisplacementObject _movement;
        private readonly ICameraMovement _camera;
    
        private Vector2 _moveDirection;
    
        public IInput Input => _input;
        
        public OnlyUpMovementRouter(IInput input, IDisplacementObject movement, ICameraMovement camera)
        {
            _input = input;
            _movement = movement;
            _camera = camera;
        }
    
        public void Update(float _)
        {
            _moveDirection = _input.MoveDirection();
            var rotateDirection = Vector3.zero;
            
            if (_input.RotateDirection(out var direction) || _camera.FirstPersonPerspective)
                rotateDirection = direction;

            if (_input.Jump())
                _movement.TryJump();
            
            if (_input.Sprint())
                _movement.TryEnableSprint(_moveDirection.x, _moveDirection.y);
            else
                _movement.DisableSprint();
            
            _camera.ChangeRotation(rotateDirection.x, rotateDirection.y);
            _camera.ChangeDistance(_input.ChangeCameraDistance());
        }

        public void FixedUpdate()
        {
            _movement.TryMove(_moveDirection.x, _moveDirection.y);
        }
    }
}
