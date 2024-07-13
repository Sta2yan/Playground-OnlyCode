using Agava.Combat;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class InAttackRange : Conditional
    {
        public SharedCombatCharacter Target;
        public SharedFloat AttackRange;

        public override TaskStatus OnUpdate()
        {
            CombatCharacter target = Target.Value;

            if (target == null)
                return TaskStatus.Failure;

            float distance = Vector3.Distance(target.transform.position, transform.position);
            return distance <= AttackRange.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
