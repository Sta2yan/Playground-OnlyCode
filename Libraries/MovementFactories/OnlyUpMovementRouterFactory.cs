using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Movement;

namespace Agava.Playground3D.MovementFactories
{
    public class OnlyUpMovementRouterFactory : MovementRouterFactory
    {
        public OnlyUpMovementRouterFactory(IInput input) : base(input)
        {
        }
        
        public override IMovementRouter Create(ICameraMovement cameraMovement, IDisplacementObject displacementObject)
        {
            return new OnlyUpMovementRouter(_input, displacementObject, cameraMovement);
        }
    }
}