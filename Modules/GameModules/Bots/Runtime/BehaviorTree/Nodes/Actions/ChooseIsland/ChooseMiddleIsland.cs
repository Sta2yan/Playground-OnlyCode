using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    internal class ChooseMiddleIsland : Action
    {
        public SharedGameObject MiddleIsland;
        public SharedGameObject TargetIsland;

        public override TaskStatus OnUpdate()
        {
            GameObject middleIsland = MiddleIsland.Value;

            if (middleIsland == null)
                return TaskStatus.Failure;

            TargetIsland.Value = middleIsland;

            return middleIsland == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
