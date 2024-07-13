using Agava.AdditionalMathValues;
using Agava.AdditionalPredefinedMethods;
using Agava.Movement;
using Agava.Playground3D.Movement;
using Agava.Playground3D.MovementFactories;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class MovementRoot : CompositeRoot
    {
        [SerializeField] private Sprint _sprint;
        [SerializeField] private PlayerMove _move;
        [SerializeField] private Jump _jump;
        [SerializeField] private CameraMovement _cameraMovement;

        private IMovementRouter _movementRouter;
        
        private IGameLoop _gameLoop;
        private IGameFixedLoop _gameFixedLoop;

        public IMovementRouter MovementRouter => _movementRouter;
        
        public void Initialize(MovementRouterFactory movementRouter)
        {
            _movementRouter = movementRouter.Create(_cameraMovement, new CharacterMovementAdapter(_move, _jump, _sprint));
        }
        
        public override void Compose()
        {
            var firstPerson = new CameraDefinitionState(0, 7, 60, 70, new FloatRange(-85, 85), new Vector3(0f, .9f, 0f), new Vector3(0, 0, 0));
            var thirdPerson = new CameraDefinitionState(7, 7, 60, 70, new FloatRange(-70, 80), new Vector3(0f, .7f, 0f), new Vector3(0, 0, 0));
            
            _cameraMovement.Initialize(firstPerson, thirdPerson, thirdPerson);

            _gameLoop = new GameLoopGroup(_movementRouter as IGameLoop);
            _gameFixedLoop = new GameFixedLoopGroup(_movementRouter as IGameFixedLoop);
        }

        private void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _gameFixedLoop?.FixedUpdate();
        }
    }
}
