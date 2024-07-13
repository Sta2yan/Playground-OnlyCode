using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class Vanish : Action
    {
        public SharedGameObject RootGameObject;
        public SharedBehaviorTree BehaviorTree;
        public SharedTransform Model;
        public SharedTransform TargetSpot;
        public SharedBool TargetReached;

        public override TaskStatus OnUpdate()
        {
            TargetSpot.Value = null;
            Model.Value.rotation = Quaternion.Euler(0f, 180f, 0f);
            BehaviorTree.Value.DisableBehavior(false);
            RootGameObject.Value.SetActive(false);
            TargetReached.Value = false;
            return TaskStatus.Success;
        }
    }
}
