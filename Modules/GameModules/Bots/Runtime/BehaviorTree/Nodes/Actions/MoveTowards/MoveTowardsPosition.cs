using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    internal class MoveTowardsPosition : Action
    {
        public SharedVector3 TargetPosition;
        public SharedPathMovement PathMovement;

        public override TaskStatus OnUpdate()
        {
            PathMovement pathMovement = PathMovement.Value;

            Vector3 targetPosition = TargetPosition.Value;

            if (targetPosition == null || pathMovement == null)
                return TaskStatus.Failure;

            bool success = pathMovement.TryExecutePathMovement(targetPosition);
            return success ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
