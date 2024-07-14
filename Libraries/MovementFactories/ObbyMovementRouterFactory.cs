using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Movement;

namespace Agava.Playground3D.MovementFactories
{
    public class ObbyMovementRouterFactory : MovementRouterFactory
    {
        public ObbyMovementRouterFactory(IInput input) : base(input)
        {
        }

        public override IMovementRouter Create(ICameraMovement cameraMovement, IDisplacementObject displacementObject)
        {
            return new ObbyMovementRouter(_input, displacementObject, cameraMovement);
        }
    }
}