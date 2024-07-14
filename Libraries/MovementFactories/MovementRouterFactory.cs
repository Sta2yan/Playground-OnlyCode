using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Movement;

namespace Agava.Playground3D.MovementFactories
{
    public abstract class MovementRouterFactory
    {
        protected readonly IInput _input;

        protected MovementRouterFactory(IInput input)
        {
            _input = input;
        }

        public abstract IMovementRouter Create(ICameraMovement cameraMovement, IDisplacementObject displacementObject);
    }
}
