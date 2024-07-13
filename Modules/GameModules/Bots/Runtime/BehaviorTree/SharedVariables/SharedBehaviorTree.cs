using BehaviorDesigner.Runtime;
using System;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedBehaviorTree : SharedVariable<BehaviorTree>
    {
        public static implicit operator SharedBehaviorTree(BehaviorTree value) => new SharedBehaviorTree { Value = value };
    }
}
