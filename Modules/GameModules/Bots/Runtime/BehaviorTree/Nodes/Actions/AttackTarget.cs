using Agava.Combat;
using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    internal class AttackTarget : Action
    {
        public SharedCombatCharacter Target;
        public SharedBotEssensials BotEssensials;

        public override TaskStatus OnUpdate()
        {
            CombatCharacter target = Target.Value;
            BotEssensials essensials = BotEssensials.Value;

            if (target == null || essensials == null)
                return TaskStatus.Failure;

            essensials.InputMimic.LookAt(target.transform);
            essensials.InputMimic.TryAttack();
            return TaskStatus.Success;
        }
    }
}
