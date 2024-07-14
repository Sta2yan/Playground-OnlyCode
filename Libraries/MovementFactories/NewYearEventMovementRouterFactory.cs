using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Movement;

namespace Agava.Playground3D.MovementFactories
{
    public class NewYearEventMovementRouterFactory : MovementRouterFactory
    {
        public NewYearEventMovementRouterFactory(IInput input) : base(input) { }

        public override IMovementRouter Create(ICameraMovement cameraMovement, IDisplacementObject displacementObject)
        {
            return new NewYearEventMovementRouter(_input, displacementObject, cameraMovement);
        }
    }
}
