using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class PositionTargetExists : Conditional
    {
        public SharedVector3 TargetPosition;

        public override TaskStatus OnUpdate()
        {
            return TargetPosition.Value.Equals(Vector3.negativeInfinity) ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
