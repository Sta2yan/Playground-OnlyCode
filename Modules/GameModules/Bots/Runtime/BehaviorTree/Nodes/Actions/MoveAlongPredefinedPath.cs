using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    internal class MoveAlongPredefinedPath : Action
    {
        public SharedPathMovement PathMovement;
        public SharedPredefinedPath PredefinedPath;

        public override TaskStatus OnUpdate()
        {
            PathMovement pathMovement = PathMovement.Value;
            PredefinedPath predefinedPathObject = PredefinedPath.Value;

            if (pathMovement == null || predefinedPathObject == null)
                return TaskStatus.Failure;

            bool result = pathMovement.TryExecutePathMovement(predefinedPathObject);
            return result ? TaskStatus.Success : TaskStatus.Success;
        }
    }
}
