using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    internal class MoveTowardsTarget<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
    {
        public TSharedObject Target;
        public SharedPathMovement PathMovementExecutor;
        public SharedBool TargetReached;

        public override TaskStatus OnUpdate()
        {
            if (Target.IsNone || PathMovementExecutor.IsNone)
                return TaskStatus.Failure;

            System.Action onPathIteractionEnd = TargetReached.IsNone? null : () => TargetReached.Value = true;
            bool success = PathMovementExecutor.Value.TryExecutePathMovement(Target.Value.transform, onPathIteractionEnd);
            return success ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
