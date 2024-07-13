using BehaviorDesigner.Runtime.Tasks;
using Agava.Combat;

namespace Agava.Playground3D.Bots
{
    internal class TargetCombatCharacterAlive : Conditional
    {
        public SharedCombatCharacter Target;

        public override TaskStatus OnUpdate()
        {
            CombatCharacter combatCharacter = Target.Value;

            if (combatCharacter == null)
                return TaskStatus.Failure;

            return combatCharacter.Alive ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
