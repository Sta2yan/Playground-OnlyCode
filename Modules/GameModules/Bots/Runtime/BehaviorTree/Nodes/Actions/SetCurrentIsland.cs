using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    public class SetCurrentIsland : Action
    {
        public SharedGameObject CurrentIsland;
        public SharedGameObject TargetIsland;

        public override TaskStatus OnUpdate()
        {
            GameObject targetIsland = TargetIsland.Value;

            if (targetIsland == null)
                return TaskStatus.Failure;

            CurrentIsland.Value = targetIsland;

            return TaskStatus.Success;
        }
    }
}
