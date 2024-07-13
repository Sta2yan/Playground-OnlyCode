using Agava.AdditionalPredefinedMethods;
using Agava.Combat;
using Agava.Input;
using Agava.Movement;
using Agava.Utils;
using Agava.Playground3D.Items;
using UnityEngine;
using Agava.Audio;
using Agava.ExperienceSystem;
using System;

namespace Agava.Playground3D.Input
{
    public class CombatInputRouter : IGameLoop
    {
        private const float ZoomHorizontalOffset = 1f;
        private const int ZoomFieldOfView = 15;

        private readonly IMagazine _magazine;
        private readonly ICombatCharacter _character;
        private readonly IInput _input;
        private readonly Hand _hand;
        private readonly KillsCountView _killsCountView;
        private readonly LookAtDirectionalCamera _look;
        private readonly CameraMovement _cameraMovement;
        private readonly int _defaultZoom;
        private readonly ICombatAnimation _combatAnimation;
        private readonly ITeamList _teamList;

        private readonly GameObject _attackButton;
        private readonly GameObject _zoomButton;

        private readonly ISoundSource _swingSoundSource;
        private readonly bool _isRedBlood = true;

        private readonly ExperienceEventsContainer _experienceEventsContainer;
        private readonly IItemExperienceEventRule _attackBotEventRule;
        private readonly ExperienceEventRule _enemyKillEventRule;

        private bool _enabled = true;
        private int _kills;

        public bool Zooming { get; private set; }

        public CombatInputRouter(IInput input, IMagazine magazine,
            ICombatCharacter character, Hand hand,
            KillsCountView killsCountView, CameraMovement cameraMovement,
            GameObject zoomButton, GameObject attackButton,
            ICombatAnimation combatAnimation, ISoundSource swingSoundSource,
            ITeamList teamList,
            ExperienceEventsContainer experienceEventsContainer,
            IItemExperienceEventRule attackBotEventRule,
            ExperienceEventRule enemyKillEventRule)
        {
            _magazine = magazine;
            _character = character;
            _input = input;

            _hand = hand;
            _killsCountView = killsCountView;
            _cameraMovement = cameraMovement;
            _defaultZoom = (int)Camera.main.fieldOfView;

            _attackButton = attackButton;
            _zoomButton = zoomButton;

            _combatAnimation = combatAnimation;

            _look = new LookAtDirectionalCamera();
            _swingSoundSource = swingSoundSource;
            _teamList = teamList;

            _experienceEventsContainer = experienceEventsContainer;
            _attackBotEventRule = attackBotEventRule;
            _enemyKillEventRule = enemyKillEventRule;
        }

        public void Update(float _)
        {
            if (_enabled == false)
                return;

            _attackButton?.SetActive(ItemInHandIs<IRangedWeapon, Gun>(out var _, out var _) || ItemInHandIs<ISword, Sword>(out var _, out var _));

            if (ItemInHandIs<IRangedWeapon, Gun>(out IItem gunItem, out var gun))
            {
                _zoomButton?.SetActive(true);

                if (TryUseGun(gun, onHit: () =>
                {
                    if (_attackBotEventRule.TryGetExperienceEvent(gunItem, out ExperienceEvent experienceEvent))
                        _experienceEventsContainer.TriggerEvent(experienceEvent);
                }))
                {
                    _combatAnimation.Shoot();
                }

                if (ChangeZoomState())
                    UpdateRotationInZoom(gun);
            }
            else
            {
                _zoomButton?.SetActive(false);
            }

            if (ItemInHandIs<ISword, Sword>(out IItem swordItem, out var sword))
            {
                if (TryUseSword(sword, onHit: () =>
                {
                    if (_attackBotEventRule.TryGetExperienceEvent(swordItem, out ExperienceEvent experienceEvent))
                        _experienceEventsContainer.TriggerEvent(experienceEvent);
                }))
                {
                    _swingSoundSource.Play();
                    _combatAnimation.Hit();
                }
            }
        }

        public void SetActive(bool value)
        {
            _enabled = value;
        }

        private bool TryUseGun(Gun gun, Action onHit = null)
        {
            if (gun.Initialized == false)
                gun.Initialize(_magazine, without: _character, false);

            if (_input.Attack() == false || gun.CanShot == false)
                return false;

            gun.Shot(_isRedBlood, onKill: () =>
            {
                _killsCountView.Add(1);
                _experienceEventsContainer.TriggerEvent(_enemyKillEventRule.ExperienceEvent());
            },
            onHit: onHit);

            return true;
        }

        private bool ChangeZoomState()
        {
            if (_input.Zoom())
            {
                _cameraMovement.ChangeHorizontalOffset(ZoomHorizontalOffset);
                _cameraMovement.ChangeFieldOfView(_defaultZoom - ZoomFieldOfView);
                _combatAnimation.EnableZoom();
                Zooming = true;
                return true;
            }

            _combatAnimation.DisableZoom();
            _cameraMovement.ResetOffset();
            Zooming = false;
            return false;
        }

        private void UpdateRotationInZoom(Component component)
        {
            _look.Execute(component.transform);
        }

        private bool TryUseSword(Sword sword, Action onHit = null)
        {
            if (_input.Attack() == false)
                return false;

            if (sword.CanAttack)
            {
                ITeam[] friendlyTeams;

                if (_teamList.TryGetFriendlyTeams(_character, out friendlyTeams) == false)
                    friendlyTeams = new ITeam[] { };

                int kills = sword.Attack(_character.Forward, without: new[] { _character }, friendlyTeams: friendlyTeams, onHit: onHit);
                _killsCountView?.Add(kills);

                if (kills > 0)
                    _experienceEventsContainer.TriggerEvent(_enemyKillEventRule.ExperienceEvent());
            }

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
