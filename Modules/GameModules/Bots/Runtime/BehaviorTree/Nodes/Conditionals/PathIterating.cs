using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    internal class PathIterating : Conditional
    {
        public SharedPathMovement PathMovement;

        public override TaskStatus OnUpdate()
        {
            PathMovement pathMovement = PathMovement.Value;

            if (pathMovement == null)
                return TaskStatus.Failure;

            return pathMovement.PathIterating ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
