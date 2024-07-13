using Agava.Input;
using Agava.Playground3D.Items;
using Agava.Playground3D.MovementFactories;
using Agava.Playground3D.BlockRouterFactories;
using Agava.Playground3D.ShopRouterFactories;
using Agava.Playground3D.InventoryRouterFactories;
using UnityEngine;
using Agava.Inventory;
using Agava.Playground3D.Input;
using Agava.DroppedItems;
using Agava.Playground3D.CombatRouterFactories;
using System.Collections.Generic;
using Agava.Combat;
using Agava.Playground3D.MainMenu;
using Agava.Tests;
using Agava.ExperienceSystem;
using Agava.Playground3D.Bots;
using Agava.Playground3D.PathFinding;
using Agava.Playground3D.CoffeeBreak;
#if YANDEX_GAMES
using Agava.WebUtility;
#endif

namespace Agava.Playground3D.CompositeRoot
{
    public class SandboxRoot : CompositeRoot
    {
        private const float StepChangeDistanceCamera = .5f;

        [SerializeField] private ItemsList _itemsList;
        [SerializeField] private CursorPresenter _cursorPresenter;
        [SerializeField] private CoffeeBreakMiniGame _coffeeBreakMiniGame;

        [Header("Roots")]
        [SerializeField] private MovementRoot _movementRoot;
        [SerializeField] private BlocksRoot _blocksRoot;
        [SerializeField] private ShopRoot _shopRoot;
        [SerializeField] private InventoryRoot _inventoryRoot;
        [SerializeField] private CombatRoot _combatRoot;
        [SerializeField] private SandboxBotRoot _sandboxBotRoot;
        [SerializeField] private SandboxInterfaceRoot _interfaceRoot;
        [SerializeField] private RagdollRoot _gravityGunRoot;
        [SerializeField] private ExperienceSystemRoot _experienceSystemRoot;
        [SerializeField] private DynamiteRoot _dynamiteRoot;
        [SerializeField] private PathfindingRoot _pathfindingRoot;

        [Header("Inventory")]
        [SerializeField] private Hand _hand;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private Transform _droppedItemRoot;
        [SerializeField] private InventoryOpenButton _inventoryOpenButton;

        [Header("Experience system")]
        [SerializeField] private LockedItemsList _lockedItemsList;
        [SerializeField] private ItemExperienceEventRule _placeBlockEventRule;
        [SerializeField] private ItemExperienceEventRule _destroyBlockEventRule;
        [SerializeField] private ItemExperienceEventRule _attackBotEventRule;
        [SerializeField] private ItemExperienceEventRule _botSpawnEventRule;
        [SerializeField] private ItemExperienceEventRule _botDeathEventRule;
        [SerializeField] private ExperienceEventRule _dynamiteExplosionEventRule;
        [SerializeField] private ExperienceEventRule _gravityGunUseEventRule;
        [SerializeField] private ExperienceEventRule _balloonPopEventRule;

        [Header("Mobile input")]
        [SerializeField] private Canvas _mobileUi;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private MobileInteractiveArea _mobileInteractiveArea;
        [SerializeField] private ButtonTrackingArea _sprint;
        [SerializeField] private ButtonTrackingArea _attack;
        [SerializeField] private ButtonTrackingArea _jump;
        [SerializeField] private ButtonTrackingArea _zoom;
        [SerializeField] private ButtonTrackingArea _drop;

        [Header("Player")]
        [SerializeField] private CombatCharacter _playerCombatCharacter;

        [Header("Portal")]
        [SerializeField] private PortalToMode _mainMenuPortal;
        [SerializeField] private GameObject _returnMainMenuPanel;

        [Header("Editor")]
        [SerializeField] private GlobalConfigStorage _config;

        private bool _mobile;

        public override void Compose()
        {
            _mobile = MobileInput();

            _itemsList.Initialize();
            var inventory = new Agava.Inventory.Inventory(_inventoryView.Slots);
            _inventoryOpenButton.Initialize(inventory);
            inventory.Visualize(_inventoryView);

            IInput input = CreateTargetInput();

            ISandboxTeamList teamList = CreateTeamList();

            if (teamList.TryGetTeam(SandboxTeamType.Player, out ISandboxTeam playerTeam))
                playerTeam.Add(_playerCombatCharacter);

            ExperienceEventsContainer experienceEventsContainer = new ExperienceEventsContainer();
            PathFindingUpdate pathFindingUpdate = new PathFindingUpdate();
            LevelGate levelGate = new LevelGate(_lockedItemsList, _config.AllContentUnlocked);

            _movementRoot.Initialize(new SandboxMovementRouterFactory(input));
            _blocksRoot.Initialize(_itemsList, new SandboxBlockRouterFactory(input, inventory, _inventoryView, _hand, experienceEventsContainer, _placeBlockEventRule, _destroyBlockEventRule, pathFindingUpdate));
            _shopRoot.Initialize(inventory, _inventoryView, new SandboxShopRouterFactory(input, _itemsList), levelGate);
            _inventoryRoot.Initialize(inventory, _inventoryView, new SandboxInventoryRouterFactory(input, _hand, _itemsList));
            _combatRoot.Initialize(inventory, _inventoryView, _mobile, new SandboxCombatRouterFactory(input, _hand, _attack.gameObject, _zoom.gameObject, teamList, experienceEventsContainer, _attackBotEventRule));
            _sandboxBotRoot.Initialize(input, teamList, experienceEventsContainer, _botSpawnEventRule, _botDeathEventRule);
            _interfaceRoot.Initialize(_itemsList, inventory, _mobile);
            _gravityGunRoot.Initialize(input, _hand, _mobile, experienceEventsContainer, _gravityGunUseEventRule);
            _dynamiteRoot.Initialize(experienceEventsContainer, _dynamiteExplosionEventRule);
            _experienceSystemRoot.Initialize(_lockedItemsList, experienceEventsContainer, levelGate);
            _pathfindingRoot.Initialize(pathFindingUpdate);
            _coffeeBreakMiniGame.Initialize(experienceEventsContainer, _balloonPopEventRule);

            _cursorPresenter.Initialize(null, _movementRoot.MovementRouter, null, null);
            _mainMenuPortal.Initialize(null, _returnMainMenuPanel);
        }

        private bool MobileInput()
        {
            bool mobileInput = false;

#if UNITY_EDITOR
            mobileInput = _config.MobileInput;
#elif YANDEX_GAMES && !UNITY_EDITOR
                mobileInput = Device.IsMobile;
#elif ANDROID_BUILD && !UNITY_EDITOR
                mobileInput = true;
#endif

            return mobileInput;
        }

        private IInput CreateTargetInput()
        {
            _mobileUi.gameObject.SetActive(_mobile);

            return _mobile
                ? new MobileInput(_movementJoystick, _mobileInteractiveArea, StepChangeDistanceCamera, _jump, _sprint, _attack, _zoom, _drop)
                : new StandaloneInput(StepChangeDistanceCamera);
        }

        private ISandboxTeamList CreateTeamList()
        {
            var playerTeam = new SandboxTeam();

            var playerFriendlyTeam = new SandboxTeam();
            playerFriendlyTeam.AddFriendlyTeam(playerFriendlyTeam);
            playerFriendlyTeam.AddFriendlyTeam(playerTeam);

            var playerAgressiveTeam = new SandboxTeam();

            var playerEnemyTeam = new SandboxTeam();
            playerEnemyTeam.AddFriendlyTeam(playerEnemyTeam);
            playerEnemyTeam.AddFriendlyTeam(playerAgressiveTeam);
            playerEnemyTeam.AddFriendlyTeam(playerFriendlyTeam);

            var neutralTeam = new SandboxTeam();
            neutralTeam.AddFriendlyTeam(neutralTeam);
            neutralTeam.AddFriendlyTeam(playerTeam);
            neutralTeam.AddFriendlyTeam(playerFriendlyTeam);
            neutralTeam.AddFriendlyTeam(playerEnemyTeam);
            neutralTeam.AddFriendlyTeam(playerAgressiveTeam);

            playerFriendlyTeam.AddFriendlyTeam(neutralTeam);
            playerEnemyTeam.AddFriendlyTeam(neutralTeam);

            Dictionary<SandboxTeamType, ISandboxTeam> teams = new Dictionary<SandboxTeamType, ISandboxTeam>()
            {
                { SandboxTeamType.Player, playerTeam },
                { SandboxTeamType.PlayerFriendly, playerFriendlyTeam },
                { SandboxTeamType.Agressive, playerAgressiveTeam },
                { SandboxTeamType.PlayerEnemy, playerEnemyTeam },
                { SandboxTeamType.Neutral, neutralTeam },
            };

            return new SandboxTeamList(teams);
        }
    }
}
