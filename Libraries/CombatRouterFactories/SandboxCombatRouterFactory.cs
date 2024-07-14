using System.Collections.Generic;
using Agava.Combat;
using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Combat;
using Agava.Playground3D.Input;
using UnityEngine;
using Agava.Audio;
using UnityEngine.UI;
using Agava.Utils;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.CombatRouterFactories
{
    public class SandboxCombatRouterFactory : CombatRouterFactory
    {
        private readonly ITeamList _teamList;
        private readonly IItemExperienceEventRule _attackBotEventRule;

        public SandboxCombatRouterFactory(IInput input, Hand hand,
            GameObject attackButton, GameObject zoomButton,
            ITeamList teamList, ExperienceEventsContainer experienceEventsContainer,
            IItemExperienceEventRule attackBotEventRule) : base(input, hand, attackButton, zoomButton, experienceEventsContainer)
        {
            _teamList = teamList;
            _attackBotEventRule = attackBotEventRule;
        }

        public override ICombatRouter Create(Dictionary<string, IMagazine> magazines, ICombatCharacter combatCharacter, ICameraMovement cameraMovement, ICombatAnimation combatAnimation, ISoundSource swordSwingSoundSource, ObjectMoveToMouse crosshair, bool isMobile, RedBlood redBlood)
        {
            return new SandboxCombatRouter(input, magazines, combatCharacter, hand, cameraMovement, zoomButton, attackButton, combatAnimation, swordSwingSoundSource, crosshair, _teamList, isMobile, redBlood, experienceEventsContainer, _attackBotEventRule);
        }
    }
}
