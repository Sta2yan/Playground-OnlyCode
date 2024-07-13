using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class NullifyTargetPosition : Action
    {
        public SharedVector3 TargetPosition;

        public override TaskStatus OnUpdate()
        {
            TargetPosition.Value = Vector3.negativeInfinity;

            return TaskStatus.Success;
        }
    }
}
