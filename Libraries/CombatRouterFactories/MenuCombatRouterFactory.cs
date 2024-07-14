using System.Collections.Generic;
using Agava.Audio;
using Agava.Combat;
using Agava.ExperienceSystem;
using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Combat;
using Agava.Playground3D.Input;
using Agava.Utils;
using UnityEngine;

namespace Agava.Playground3D.CombatRouterFactories
{
    public class MenuCombatRouterFactory : CombatRouterFactory
    {
        private readonly Sword _sword;
        private readonly ExperienceEvent _experienceEvent;
        
        public MenuCombatRouterFactory(IInput input, Hand hand, GameObject attackButton, GameObject zoomButton, ExperienceEventsContainer experienceEventsContainer, Sword sword, ExperienceEvent experienceEvent) : base(input, hand, attackButton, zoomButton, experienceEventsContainer)
        {
            _sword = sword;
            _experienceEvent = experienceEvent;
        }

        public override ICombatRouter Create(Dictionary<string, IMagazine> magazines, ICombatCharacter combatCharacter, ICameraMovement cameraMovement,
            ICombatAnimation combatAnimation, ISoundSource swordSwingSoundSource, ObjectMoveToMouse crosshair, bool isMobile,
            RedBlood redBlood = null)
        {
            return new MenuCombatRouter(input, combatCharacter, _sword, combatAnimation, swordSwingSoundSource, attackButton, experienceEventsContainer, _experienceEvent);
        }
    }
}