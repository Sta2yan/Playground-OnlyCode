using System;
using Agava.AdditionalPredefinedMethods;
using Agava.Audio;
using Agava.Combat;
using Agava.ExperienceSystem;
using Agava.Input;
using UnityEngine;

namespace Agava.Playground3D.Combat
{
    public class MenuCombatRouter : ICombatRouter, IGameLoop
    {
        private readonly IInput _input;
        private readonly ICombatCharacter _character;
        private readonly Sword _sword;
        private readonly ExperienceEventsContainer _experienceEventsContainer;
        private readonly IItemExperienceEventRule _attackBotEventRule;
        private readonly ICombatAnimation _combatAnimation;
        private readonly ISoundSource _swordSwingSoundSource;
        private readonly GameObject _attackButton;
        private readonly ExperienceEvent _experienceEvent;
        
        public MenuCombatRouter(IInput input, ICombatCharacter character, Sword sword, ICombatAnimation combatAnimation, ISoundSource soundSource, GameObject attackButton, ExperienceEventsContainer experienceEventsContainer, ExperienceEvent experienceEvent)
        {
            _input = input;
            _character = character;
            _sword = sword;
            _combatAnimation = combatAnimation;
            _swordSwingSoundSource = soundSource;
            _attackButton = attackButton;
            _experienceEventsContainer = experienceEventsContainer;
            _experienceEvent = experienceEvent;
        }
        
        public void Update(float deltaTime)
        {
            _attackButton.SetActive(_sword.gameObject.activeSelf);
            
            if (TryUseSword(_sword, onHit: () =>
            {
                _experienceEventsContainer.TriggerEvent(_experienceEvent);
            }))
            {
                _combatAnimation.Hit();
                _swordSwingSoundSource.Play();
            }
        }
        
        private bool TryUseSword(Sword sword, Action onHit = null)
        {
            if (_input.Attack() == false)
                return false;

            if (sword.gameObject.activeSelf == false)
                return false;
            
            if (sword.CanAttack)
                sword.Attack(_character.Forward, without: new[] { _character }, friendlyTeams: null, onHit: onHit);

            return true;
        }
    }
}