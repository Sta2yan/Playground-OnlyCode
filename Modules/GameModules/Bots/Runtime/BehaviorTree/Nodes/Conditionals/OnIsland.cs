using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    public class OnIsland : Conditional
    {
        public SharedGameObject CurrentIsland;
        public SharedGameObject TargetIsland;

        public override TaskStatus OnUpdate()
        {
            GameObject currentIsland = CurrentIsland.Value;
            GameObject targetIsland = TargetIsland.Value;

            if (currentIsland == null || targetIsland == null)
                return TaskStatus.Failure;

            return currentIsland == targetIsland ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
