using Agava.AdditionalPredefinedMethods;
using Agava.Blocks;
using Agava.Input;
using Agava.Playground3D.Input;
using Agava.Playground3D.Items;
using System.Collections.Generic;
using UnityEngine;
using Agava.Combat;
using Agava.Movement;
using System.Linq;
using Agava.Playground3D.Sandbox.UserInterface;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.Bots
{
    internal class SandboxBotRouter : IBotRouter, IGameLoop
    {
        private const float BotDestructionTime = 4f;
        private const float PlaceDistance = 20f;
        private const int MaxBotsCount = 16;
        private const float SpawnBotsDistance = 0.25f;

        private readonly IInput _input;
        private readonly BotInstantiation<SandboxBotComposer> _botInstantiation;
        private readonly Hand _hand;
        private readonly BlockAnimator _animation;
        private readonly Transform _botParent;
        private readonly DelayedDestruction _delayedDestruction;
        private readonly ICameraMovement _cameraMovement;
        private readonly LayerMask _groundLayerMask;
        private readonly CountView.CountView _botsCountView;
        private readonly Transform _playerModel;
        private readonly LayerMask _characterLayerMask;
        private readonly BotsCommunication _botsCommunication;

        private readonly ExperienceEventsContainer _experienceEventsContainer;
        private readonly IItemExperienceEventRule _botSpawnEventRule;
        private readonly IItemExperienceEventRule _botDeathEventRule;

        private List<SandboxBot> _bots;

        public SandboxBotRouter(IInput input,
            BotInstantiation<SandboxBotComposer> botInstantiation,
            Hand hand, BlockAnimator blockAnimator,
            Transform botParent, DelayedDestruction delayedDestruction,
            ICameraMovement cameraMovement, LayerMask groundLayerMask, CountView.CountView botsCountView,
            Transform playerModel,
            LayerMask charactersLayerMask, BotsCommunication botsCommunication,
            ExperienceEventsContainer experienceEventsContainer,
            IItemExperienceEventRule botSpawnEventRule, IItemExperienceEventRule botDeathEventRule)
        {
            _bots = new();
            _input = input;
            _botInstantiation = botInstantiation;
            _hand = hand;
            _animation = blockAnimator;
            _botParent = botParent;
            _delayedDestruction = delayedDestruction;
            _cameraMovement = cameraMovement;
            _groundLayerMask = groundLayerMask;
            _botsCountView = botsCountView;
            _botsCountView.Initialize(MaxBotsCount);
            _playerModel = playerModel;
            _characterLayerMask = charactersLayerMask;
            _botsCommunication = botsCommunication;
            _experienceEventsContainer = experienceEventsContainer;
            _botSpawnEventRule = botSpawnEventRule;
            _botDeathEventRule = botDeathEventRule;
        }

        public void Update(float _)
        {
            if (TryGetBotTemplate(out var botTemplate))
            {
                if (_input.UseItem())
                {
                    if (PositionPlace(out Vector3 position))
                    {
                        if (_bots.Count < MaxBotsCount)
                        {
                            if (TryGetSpawnerByBot(botTemplate, out IBotSpawn botSpawnItem))
                            {
                                if (_botSpawnEventRule.TryGetExperienceEvent(botSpawnItem, out ExperienceEvent botSpawnEvent))
                                    _experienceEventsContainer.TriggerEvent(botSpawnEvent);
                            }

                            InstantiateBot(botTemplate, position);
                            _animation.Place();
                        }
                    }
                }
            }

            foreach (SandboxBot bot in _bots)
            {
                if (bot == null)
                    continue;

                if (bot.CombatCharacter.Alive == false)
                {
                    _delayedDestruction.Destroy(bot.CombatCharacter, BotDestructionTime, () =>
                    {
                        if (TryGetSpawnerByBot(bot, out IBotSpawn botSpawnItem))
                        {
                            if (_botDeathEventRule.TryGetExperienceEvent(botSpawnItem, out ExperienceEvent botDeathEvent))
                                _experienceEventsContainer.TriggerEvent(botDeathEvent);
                        }
                    });
                }
            }

            _bots.RemoveAll(bot => bot == null);

            int botsCount = _bots.Count(bot => bot.CombatCharacter.Alive);
            _botsCountView.SetActive(botsCount > 0);
            _botsCountView.SetCount(botsCount);

            if (_botsCommunication.Selected)
            {
                foreach (SandboxBot bot in _bots)
                {
                    Object.Destroy(bot.gameObject);
                }

                _bots.Clear();
                _botsCommunication.Unselect();
            }
        }

        private bool TryGetBotTemplate(out SandboxBot targetBotTemplate)
        {
            targetBotTemplate = default;

            if (_hand.CurrentItem.Is<IBotSpawn>() == false)
                return false;

            GameObject botTemplate = (_hand.CurrentItem as IBotSpawn).BotTemplate;

            if (botTemplate == null)
                return false;

            return botTemplate.TryGetComponent(out targetBotTemplate);
        }

        private void InstantiateBot(SandboxBot botTemplate, Vector3 position)
        {
            SandboxBot bot = Object.Instantiate(botTemplate, _botParent);
            _botInstantiation.InstantiateBot(bot, position);
            bot.transform.LookAt(_playerModel);
            _bots.Add(bot);
            Transform botTransform = bot.transform;
            Vector3 playerPosition = _playerModel.position;
            botTransform.LookAt(new Vector3(playerPosition.x, botTransform.position.y, playerPosition.z));
        }

        private bool PositionPlace(out Vector3 position)
        {
            position = Vector3.zero;

            var cameraRay = RayByCameraPerson();

            if (Physics.Raycast(cameraRay, out var hitInfo, PlaceDistance, _groundLayerMask) == false)
                return false;

            if (hitInfo.collider == null)
                return false;

            if (hitInfo.collider.isTrigger)
                return false;

            position = hitInfo.point + Vector3.up;

            if (Physics.OverlapSphere(position, SpawnBotsDistance, _characterLayerMask).Count() > 0)
                return false;

            return true;
        }

        private Ray RayByCameraPerson()
        {
            Vector2 inputPosition = _cameraMovement.FirstPersonPerspective ? _input.FirstPersonInput(true) : _input.ThirdPersonInput(true);
            return _cameraMovement.CameraMain.ScreenPointToRay(inputPosition);
        }

        private bool TryGetSpawnerByBot(SandboxBot bot, out IBotSpawn botSpawnItem)
        {
            botSpawnItem = null;

            if (bot == null)
                return false;

            botSpawnItem = bot.SpawnItem;
            return botSpawnItem != null;
        }
    }
}
