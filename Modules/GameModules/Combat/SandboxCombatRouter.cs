using System;
using System.Collections.Generic;
using Agava.AdditionalPredefinedMethods;
using Agava.Combat;
using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.Input;
using Agava.Playground3D.Items;
using Agava.Utils;
using UnityEngine;
using Agava.Audio;
using UnityEngine.UI;
using Agava.Playground3D.GravyGun;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.Combat
{
    public class SandboxCombatRouter : ICombatRouter, IGameLoop
    {
        private const float ZoomHorizontalOffset = 1f;
        private const int ZoomFieldOfView = 15;

        private readonly Dictionary<string, IMagazine> _magazines;
        private readonly ICombatCharacter _character;
        private readonly IInput _input;
        private readonly Hand _hand;
        private readonly LookAtDirectionalCamera _look;
        private readonly ICameraMovement _cameraMovement;
        private readonly int _defaultZoom;
        private readonly ICombatAnimation _combatAnimation;

        private readonly ObjectMoveToMouse _crosshair;
        private readonly ITeamList _teamList;
        private bool _isMobile;
        private readonly GameObject _attackButton;
        private readonly GameObject _zoomButton;
        private readonly RedBlood _redBlood;
        private readonly ISoundSource _swordSwingSoundSource;

        private readonly ExperienceEventsContainer _experienceEventsContainer;
        private readonly IItemExperienceEventRule _attackBotEventRule;

        private bool _enabled = true;

        public bool Zooming { get; private set; }

        public SandboxCombatRouter(IInput input,
            Dictionary<string, IMagazine> magazines,
            ICombatCharacter character,
            Hand hand,
            ICameraMovement cameraMovement,
            GameObject zoomButton,
            GameObject attackButton,
            ICombatAnimation combatAnimation,
            ISoundSource swordSwingSoundSource,
            ObjectMoveToMouse crosshair,
            ITeamList teamList,
            bool isMobile,
            RedBlood redBlood,
            ExperienceEventsContainer experienceEventsContainer,
            IItemExperienceEventRule attackBotEventRule)
        {
            _magazines = magazines;
            _character = character;
            _input = input;

            _hand = hand;
            _cameraMovement = cameraMovement;
            _defaultZoom = (int)Camera.main.fieldOfView;

            _attackButton = attackButton;
            _zoomButton = zoomButton;

            _combatAnimation = combatAnimation;

            _look = new LookAtDirectionalCamera();
            _swordSwingSoundSource = swordSwingSoundSource;

            _teamList = teamList;
            _isMobile = isMobile;
            _crosshair = crosshair;
            _crosshair.Initialize(_isMobile);

            _redBlood = redBlood;

            _experienceEventsContainer = experienceEventsContainer;
            _attackBotEventRule = attackBotEventRule;
        }

        public void Update(float _)
        {
            if (_enabled == false)
                return;

            _attackButton?.SetActive((ItemInHandIs<IRangedWeapon, Gun>(out var _, out var _) && !_isMobile) /*|| ItemInHandIs<ISword, Sword>(out var _)*/); ;

            if (ItemInHandIs<IRangedWeapon, Gun>(out IItem gunItem, out var gun))
            {
                if (!_isMobile)
                {
                    _crosshair.gameObject.SetActive(true);
                    _crosshair.SetSprite(gun.Crosshair);
                }

                switch (gun.HandType)
                {
                    case HandTypeGun.OneHanded:
                        _combatAnimation.EnableZoom();
                        break;
                    case HandTypeGun.TwoHanded:
                        _combatAnimation.EnableZoomTwoHand();
                        break;
                }

                Initialize(gun);

                TryUse(gun, onHit: () =>
                {
                    if (_attackBotEventRule.TryGetExperienceEvent(gunItem, out ExperienceEvent experienceEvent))
                        _experienceEventsContainer.TriggerEvent(experienceEvent);
                });
            }
            else
            {
                _crosshair.gameObject.SetActive(false);
                _combatAnimation.DisableZoom();
            }

            if (ItemInHandIs<ISword, Sword>(out IItem swordItem, out var sword))
            {
                if (TryUseSword(sword, onHit: () =>
                {
                    if (_attackBotEventRule.TryGetExperienceEvent(swordItem, out ExperienceEvent experienceEvent))
                        _experienceEventsContainer.TriggerEvent(experienceEvent);
                }))
                {
                    _combatAnimation.Hit();
                    _swordSwingSoundSource.Play();
                }
            }
        }

        private void Initialize(Gun gun)
        {
            if (gun.Initialized == false)
            {
                switch (gun)
                {
                    case CrossbowGun:
                        gun.Initialize(_magazines[nameof(CrossbowGun)], without: _character, _isMobile);
                        break;
                    case PistolGun:
                        gun.Initialize(_magazines[nameof(PistolGun)], without: _character, _isMobile);
                        break;
                    case Shotgun:
                        gun.Initialize(_magazines[nameof(Shotgun)], without: _character, _isMobile);
                        break;
                    case MachineGun:
                        gun.Initialize(_magazines[nameof(MachineGun)], without: _character, _isMobile);
                        break;
                    default:
                        throw new InvalidOperationException("Weapon type not found!");
                }
            }
        }

        private bool TryUse(Gun gun, Action onHit = null)
        {
            if (gun is MachineGun)
            {
                if (_input.PressedAttack() == false || gun.CanShot == false)
                    return false;
            }
            else
            {
                if (_input.Attack() == false || gun.CanShot == false)
                    return false;
            }

            gun.Shot(isRedBlood: _redBlood.IsRedBlood, onHit: onHit);
            return true;
        }

        private void UpdateRotationInZoom(Component component)
        {
            _look.Execute(component.transform);
        }

        private bool TryUseSword(Sword sword, Action onHit = null)
        {
            if (_input.Attack() == false)
                return false;

            ITeam[] friendlyTeams;

            if (_teamList.TryGetFriendlyTeams(_character, out friendlyTeams) == false)
                friendlyTeams = new ITeam[] { };

            if (sword.CanAttack)
                sword.Attack(_character.Forward, without: new[] { _character }, friendlyTeams: friendlyTeams, onHit: onHit);

            return true;
        }

        private bool ItemInHandIs<TTargetItem, TTarget>(out IItem item, out TTarget targetItem) where TTargetItem : IItem where TTarget : class
        {
            item = default;
            targetItem = default;

            if (_hand.CurrentItem.Is<TTargetItem>() == false)
                return false;

            if (_hand.ItemInstance.TryGetComponent(out TTarget itemFound) == false)
                return false;

            item = _hand.CurrentItem;
            targetItem = itemFound;
            return true;
        }
    }
}
