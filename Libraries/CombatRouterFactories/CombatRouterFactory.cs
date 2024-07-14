using System.Collections.Generic;
using Agava.Combat;
using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Combat;
using Agava.Playground3D.Input;
using UnityEngine;
using Agava.Audio;
using UnityEngine.UI;
using System;
using Agava.Utils;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.CombatRouterFactories
{
    public abstract class CombatRouterFactory
    {
        protected IInput input;
        protected Hand hand;
        protected GameObject zoomButton;
        protected GameObject attackButton;
        protected ExperienceEventsContainer experienceEventsContainer;

        public CombatRouterFactory(IInput input, Hand hand, GameObject attackButton, GameObject zoomButton, ExperienceEventsContainer experienceEventsContainer)
        {
            this.input = input;
            this.hand = hand;
            this.attackButton = attackButton;
            this.zoomButton = zoomButton;
            this.experienceEventsContainer = experienceEventsContainer;
        }

        public abstract ICombatRouter Create(Dictionary<string, IMagazine> magazines, ICombatCharacter combatCharacter, ICameraMovement cameraMovement, ICombatAnimation combatAnimation, ISoundSource swordSwingSoundSource, ObjectMoveToMouse crosshair, bool isMobile, RedBlood redBlood = null);

        public ICombatRouter Create(Dictionary<string, IMagazine> magazines, CombatCharacter playerCombatCharacter, CameraMovement cameraMovement, CombatAnimator combatAnimator, SoundSource swordSwingSoundSource, Image crosshair)
        {
            throw new NotImplementedException();
        }
    }
}
