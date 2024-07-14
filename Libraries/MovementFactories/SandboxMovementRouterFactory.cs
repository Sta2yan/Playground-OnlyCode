using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Movement;

namespace Agava.Playground3D.MovementFactories
{
    public class SandboxMovementRouterFactory : MovementRouterFactory
    {
        public SandboxMovementRouterFactory(IInput input) : base(input) { }

        public override IMovementRouter Create(ICameraMovement cameraMovement, IDisplacementObject displacementObject)
        {
            return new SandboxMovementRouter(_input, displacementObject, cameraMovement);
        }
    }
}
