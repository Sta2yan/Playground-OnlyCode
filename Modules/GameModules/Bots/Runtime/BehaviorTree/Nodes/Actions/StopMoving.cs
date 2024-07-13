using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    internal class StopMoving : Action
    {
        public SharedPathMovement PathMovement;
        public SharedBotEssensials BotEssensials;

        public override TaskStatus OnUpdate()
        {
            PathMovement pathMovement = PathMovement.Value;
            BotEssensials essensials = BotEssensials.Value;

            if (pathMovement == null || essensials == null)
                return TaskStatus.Failure;

            pathMovement.StopMovement();

            return TaskStatus.Success;
        }
    }
}
