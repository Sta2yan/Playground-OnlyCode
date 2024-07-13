using System;
using System.Collections;
using System.Collections.Generic;
using Agava.AdditionalPredefinedMethods;
using Agava.Blocks;
using Agava.DroppedItems;
using Agava.Inventory;
using Agava.Movement;
using Agava.AdditionalMathValues;
using Agava.Combat;
using Agava.Customization;
using Agava.Input;
using Agava.Leaderboard;
using Agava.Playground3D.Input;
using Agava.Playground3D.Items;
using Agava.Shop;
using UnityEngine;
using UnityEngine.UI;
using Agava.Playground3D.Bots;
using Agava.Playground3D.BedWars.Combat;
using Agava.Audio;
using Agava.Tests;
using Agava.ExperienceSystem;
using Agava.Playground3D.CoffeeBreak;
#if YANDEX_GAMES
using Agava.WebUtility;
#endif

namespace Agava.Playground3D.CompositeRoot
{
    internal class BedWarsRoot : CompositeRoot
    {
        private const float StepChangeDistanceCamera = 0.5f;
        private const float BlockPlaceDistance = 5f;
        private const int MainInventoryCapacity = 33;

        [Header("Common")]
        [SerializeField] private ItemsList _itemsList;
        [SerializeField] private Canvas _mobileUi;
        [SerializeField] private CursorPresenter _cursorPresenter;
        [SerializeField] private ExperienceSystemRoot _experienceSystemRoot;
        [SerializeField] private CoffeeBreakMiniGame _coffeeBreakMiniGame;

        [Header("Character")]
        [SerializeField] private DroppedItemFactory _droppedItemsFromUser;
        [SerializeField] private Transform _blockPlacePoint;
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Sprint _sprint;
        [SerializeField] private PlayerMove _move;
        [SerializeField] private Jump _jump;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private TakenItemsList _takenItemsList;

        [Header("Customization")]
        [SerializeField] private List<SkinList> _skinLists;

        [Header("Combat")]
        [SerializeField, Min(0f)] private float _respawnDelay = 1.5f;
        [SerializeField] private CombatCharacter _playerCombatCharacter;
        [SerializeField] private BulletItem _crossbowBullet;
        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _zoomButton;
        [SerializeField] private MonoBehaviour _combatAnimator;
        [SerializeField] private BedWarsTeam[] _teams;
        [SerializeField] private SoundSource _swordSwingSoundSource;

        [Header("Blocks")]
        [SerializeField] private DroppedItemFactory _droppedItemsFromPosition;
        [SerializeField] private LockForPlaceBlocks _lockForPlaceBlocks;
        [SerializeField] private OutlineBlockView _outlineBlockView;
        [SerializeField] private BoxGridAvailableArea _gridAvailableArea;
        [SerializeField] private GridSetup _gridSetup;
        [SerializeField] private GridBlockDrawRule _blockDrawRule;
        [SerializeField] private MonoBehaviour _blockAnimator;
        [SerializeField] private Transform _healthViewRoot;
        [SerializeField] private GameObject _dropButton;
        [SerializeField] private Image _blockHealth;
        [SerializeField] private BlockDamageSource _blockDamageSource;
        [SerializeField] private LayerMask _placeBlock;
        [SerializeField] private SoundSource _placeBlockSoundSource;

        [Header("Shop")]
        [SerializeField] private ShopTrigger[] _shopTriggers;
        [SerializeField] private Agava.Shop.Shop _shop;

        [Header("Inventory")]
        [SerializeField] private Hand _hand;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private List<SlotView> _fastAccessSlotViews;
        [SerializeField] private Transform _droppedItemRoot;
        [SerializeField] private InventoryOpenButton _inventoryOpenButton;

        [Header("UserInterface")]
        [SerializeField] private UserInterface.BedWarsUserInterface _userInterface;
        [SerializeField] private KillsCountView _killsCountView;

        [Header("MobileInput")]
        [SerializeField] private MobileInteractiveArea _mobileInteractiveArea;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private ButtonTrackingArea _sprintArea;
        [SerializeField] private ButtonTrackingArea _attackArea;
        [SerializeField] private ButtonTrackingArea _jumpArea;
        [SerializeField] private ButtonTrackingArea _zoomArea;
        [SerializeField] private ButtonTrackingArea _dropArea;

        [Header("Experience system")]
        [SerializeField] private LockedItemsList _lockedItemsList;
        [SerializeField] private ItemExperienceEventRule _placeBlockEventRule;
        [SerializeField] private ItemExperienceEventRule _destroyBlockEventRule;
        [SerializeField] private ItemExperienceEventRule _attackBotEventRule;
        [SerializeField] private ItemExperienceEventRule _pickUpItemEventRule;
        [SerializeField] private ExperienceEventRule _enemyDeathEventRule;
        [SerializeField] private ExperienceEventRule _balloonPopEventRule;
        [SerializeField] private ExperienceEventRule _playerWonEventRule;

        [Header("Bots")]
        [SerializeField] private BedWarsBot[] _bots;

        [Header("DeviceBehavior")]
        [SerializeField] private List<GameObject> _desktopViewObjects;
        [SerializeField] private List<GameObject> _mobileViewObjects;

        [Header("Test")]
        [SerializeField] private GlobalConfigStorage _config;

        private CharacterMovementAdapter _characterMovementAdapter;
        private BlocksCommunication _blocksCommunication;
        private Dictionary<BedWarsBot, GameObject> _botIslands;
        private ExperienceEventsContainer _experienceEventsContainer;
        private List<IDisposable> _disposeObjects;
        private IGameLoop _gameLoop;
        private IGameLoop _gameLateLoop;
        private IGameFixedLoop _gameFixedLoop;
        private bool _mobile;

        public IReadOnlyDictionary<BedWarsBot, GameObject> BotIslands => _botIslands;
        public BlocksCommunication BlocksCommunication => _blocksCommunication;
        public IBedWarsTeamList TeamList { get; private set; }

        private ICombatAnimation CombatAnimation => (ICombatAnimation)_combatAnimator;
        private IBlockAnimation BlockAnimation => (IBlockAnimation)_blockAnimator;

        private void OnValidate()
        {
            if (_combatAnimator && _combatAnimator is not ICombatAnimation)
            {
                Debug.LogError(nameof(_combatAnimator) + " needs to implement " + nameof(ICombatAnimation));
                _combatAnimator = null;
            }

            if (_blockAnimator && _blockAnimator is not IBlockAnimation)
            {
                Debug.LogError(nameof(_blockAnimator) + " needs to implement " + nameof(IBlockAnimation));
                _blockAnimator = null;
            }
        }

        public override void Compose()
        {
            _mobile = MobileInput();

            _botIslands = new Dictionary<BedWarsBot, GameObject>();

            var delayedRespawn = new DelayedRespawn(this, _respawnDelay);

            _teams[0].Initialize(new[] { _playerCombatCharacter }, delayedRespawn);

            IEnumerable<ICombatCharacter> combatCharacters;
            int botIndex;

            for (int i = 1; i < _teams.Length; i++)
            {
                botIndex = i - 1;

                if (botIndex >= _bots.Length)
                {
                    combatCharacters = Array.Empty<ICombatCharacter>();
                }
                else
                {
                    combatCharacters = new ICombatCharacter[] { _bots[botIndex].CombatCharacter };
                    _botIslands.Add(_bots[botIndex], _teams[i].gameObject);
                }

                _teams[i].Initialize(combatCharacters, delayedRespawn);
            }

            TeamList = new BedWarsTeamList(_teams, _teams[0]);

            _characterMovementAdapter = new CharacterMovementAdapter(_move, _jump, _sprint);

            var input = CreateTargetInput();

            _experienceEventsContainer = new ExperienceEventsContainer();

            var inventory = new Agava.Inventory.Inventory(MainInventoryCapacity);
            _inventoryOpenButton.Initialize(inventory);
            inventory.Visualize(_inventoryView);

            var grid = new BlockGrid(_gridAvailableArea);
            var gridView = new GridView(_itemsList, _blockDrawRule);

            _itemsList.Initialize();
            _shop.Initialize(new ItemsWalletAdapter(inventory, _inventoryView));

            var blockHealthView = new BlockHealthView(_healthViewRoot, _blockHealth);
            _blocksCommunication = new BlocksCommunication(grid, gridView, blockHealthView);

            _gridSetup.Initialize(_blocksCommunication, _itemsList);
            _gridSetup.UploadBlocks();

            var droppedItemCommunication = new DroppedItemCommunication(_droppedItemRoot, _droppedItemsFromUser, _droppedItemsFromPosition);
            var blockRules = new BlockRules(_characterRoot, grid, _lockForPlaceBlocks, BlockPlaceDistance);
            var crossbowMagazine = new Magazine(_crossbowBullet, inventory, _inventoryView);

            var firstPerson = new CameraDefinitionState(0, 7, 60, 70, new FloatRange(-85, 85), new Vector3(0f, .9f, 0f), new Vector3(0, 0, 0));
            var thirdPerson = new CameraDefinitionState(7, 7, 60, 70, new FloatRange(-70, 80), new Vector3(0f, .7f, 0f), new Vector3(0, 0, 0));

            _cameraMovement.Initialize(firstPerson, thirdPerson, thirdPerson);
            _outlineBlockView.Init(grid);

            var personViewControl = new PersonViewControl(_cameraMovement, _skinLists.ToArray());

            foreach (var slotView in _fastAccessSlotViews)
                slotView.gameObject.AddComponent<InventorySlotInput>().Init(slotView, inventory, _inventoryView);

            _userInterface.Initialize(_respawnDelay, TeamList);

            var inventoryInputRouter = new InventoryInputRouter(input, inventory, _inventoryView, droppedItemCommunication, _takenItemsList, _itemsList, _hand, _experienceEventsContainer, _pickUpItemEventRule);
            var shopInputRouter = new ShopInputRouter(input, _shop, _shopTriggers, inventory, _inventoryView, _itemsList);
            var combatInputRouter = new CombatInputRouter(input, crossbowMagazine, _playerCombatCharacter, _hand, _killsCountView, _cameraMovement, _zoomButton, null, CombatAnimation, _swordSwingSoundSource, TeamList, _experienceEventsContainer, _attackBotEventRule, _enemyDeathEventRule);

            foreach (var desktopViewObject in _desktopViewObjects)
                desktopViewObject.SetActive(!_mobile);

            foreach (var mobileViewObject in _mobileViewObjects)
                mobileViewObject.SetActive(_mobile);

            var blockInputRouter = new BlockInputRouter(input, blockRules, _blocksCommunication, _cameraMovement, inventory, _inventoryView, _itemsList, _placeBlock, _outlineBlockView, droppedItemCommunication, _blockPlacePoint, _cameraPivot, _hand, !_mobile, _dropButton, BlockAnimation, _placeBlockSoundSource, _blockDamageSource, _experienceEventsContainer, _placeBlockEventRule, _destroyBlockEventRule);
            var movementInputRouter = new BedWarsMovementRouter(input, _characterMovementAdapter, _cameraMovement, !_mobile);

            _experienceSystemRoot.Initialize(_lockedItemsList, _experienceEventsContainer);

            _coffeeBreakMiniGame.Initialize(_experienceEventsContainer, _balloonPopEventRule);

            _cursorPresenter.Initialize(TeamList, movementInputRouter, blockInputRouter, combatInputRouter);

            StartCoroutine(WaitingWin(TeamList));

            _gameLoop = new GameLoopGroup(new IGameLoop[]
            {
                inventoryInputRouter,
                movementInputRouter,
                combatInputRouter,
                blockInputRouter,
                shopInputRouter,
                blockHealthView,
                gridView,
            });

            _gameFixedLoop = new GameFixedLoopGroup(new IGameFixedLoop[]
            {
                movementInputRouter,
            });

            _gameLateLoop = new GameLoopGroup(new IGameLoop[]
            {
                personViewControl,
            });

            _disposeObjects = new List<IDisposable>
            {
               inventoryInputRouter
            };
        }

        private bool MobileInput()
        {
            bool mobileInput = _config.MobileInput;

#if YANDEX_GAMES && !UNITY_EDITOR
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
                ? new MobileInput(_movementJoystick, _mobileInteractiveArea, StepChangeDistanceCamera, _jumpArea, _sprintArea, _attackArea, _zoomArea, _dropArea)
                : new StandaloneInput(StepChangeDistanceCamera);
        }

        private void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _gameFixedLoop?.FixedUpdate();
        }

        private void LateUpdate()
        {
            _gameLateLoop?.Update(Time.deltaTime);
        }

        private IEnumerator WaitingWin(IBedWarsTeamList teamList)
        {
            yield return new WaitUntil(() => teamList.PlayerWin);

            LeaderboardSettings.AddScore(30);
            _experienceEventsContainer.TriggerEvent(_playerWonEventRule.ExperienceEvent());
        }

        private void OnDestroy()
        {
            foreach (var destroyableObject in _disposeObjects)
                destroyableObject.Dispose();
        }
    }
}
