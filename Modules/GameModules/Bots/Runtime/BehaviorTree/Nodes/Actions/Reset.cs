using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class Reset : Action
    {
        public SharedBotEssensials BotEssensials;
        public SharedGameObject CurrentIsland;
        public SharedGameObject TeamIsland;
        public SharedCombatCharacter TargetCombatCharacter;
        public SharedVector3 TargetPosition;
        public SharedGameObject TargetIsland;
        public SharedResourceGenerator SharedResourceGenerator;
        public SharedPathMovement PathMovement;

        public override TaskStatus OnUpdate()
        {
            BotEssensials botEssensials = BotEssensials.Value;
            GameObject teamIsland = TeamIsland.Value;
            PathMovement pathMovement = PathMovement.Value;

            if (botEssensials == null || teamIsland == null || pathMovement == null)
                return TaskStatus.Failure;

            CurrentIsland.Value = teamIsland;
            TargetCombatCharacter.Value = null;
            TargetPosition.Value = Vector3.negativeInfinity;
            TargetIsland.Value = null;
            SharedResourceGenerator.Value = null;
            pathMovement.StopMovement();

            return TaskStatus.Success;
        }
    }
}
