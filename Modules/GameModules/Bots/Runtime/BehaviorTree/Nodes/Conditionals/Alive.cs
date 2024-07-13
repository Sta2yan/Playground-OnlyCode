using BehaviorDesigner.Runtime.Tasks;
using Agava.Combat;

namespace Agava.Playground3D.Bots
{
    internal class Alive : Conditional
    {
        public SharedCombatCharacter SelfCombatCharacter;

        public override TaskStatus OnUpdate()
        {
            CombatCharacter combatCharacter = SelfCombatCharacter.Value;

            if (combatCharacter == null)
                return TaskStatus.Failure;

            return combatCharacter.Alive ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
