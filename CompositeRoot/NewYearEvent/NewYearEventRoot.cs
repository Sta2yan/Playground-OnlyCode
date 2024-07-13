using Agava.Combat;
using Agava.ExperienceSystem;
using Agava.Input;
using Agava.Playground3D.CoffeeBreak;
using Agava.Playground3D.Input;
using Agava.Playground3D.MovementFactories;
using Agava.Playground3D.NewYearEvent;
using Agava.Tests;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class NewYearEventRoot : CompositeRoot
    {
        private const float StepChangeDistanceCamera = .5f;

        [SerializeField] private CursorPresenter _cursorPresenter;
        [SerializeField] private CoffeeBreakMiniGame _coffeeBreakMiniGame;
        [SerializeField] private CollectableItemSpawnPoint[] _collectableItemsSpawnPoints;

        [Header("Roots")]
        [SerializeField] private MovementRoot _movementRoot;
        [SerializeField] private CollectableItemsRoot _collectableItemsRoot;
        [SerializeField] private NewYearEventBotRoot _botRoot;
        [SerializeField] private ExperienceSystemRoot _experienceSystemRoot;

        [Header("Experience system")]
        [SerializeField] private LockedItemsList _lockedItemsList;
        [SerializeField] private ExperienceEventRule _balloonPopEventRule;

        [Header("Mobile input")]
        [SerializeField] private Canvas _mobileUi;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private MobileInteractiveArea _mobileInteractiveArea;
        [SerializeField] private ButtonTrackingArea _sprint;
        [SerializeField] private ButtonTrackingArea _jump;
        [SerializeField] private ButtonTrackingArea _zoom;

        [Header("Player")]
        [SerializeField] private CombatCharacter _playerCombatCharacter;

        [Header("Editor")]
        [SerializeField] private GlobalConfigStorage _config;

        private bool _mobile;

        public override void Compose()
        {
            _mobile = MobileInput();

            IInput input = CreateTargetInput();

            ExperienceEventsContainer experienceEventsContainer = new ExperienceEventsContainer();
            LevelGate levelGate = new LevelGate(_lockedItemsList, _config.AllContentUnlocked);

            _movementRoot.Initialize(new NewYearEventMovementRouterFactory(input));

            CollectableItemsBotContainer container = new CollectableItemsBotContainer(_collectableItemsSpawnPoints);

            _botRoot.Initialize(container);
            _collectableItemsRoot.Initialize(_collectableItemsSpawnPoints, container);
            _experienceSystemRoot.Initialize(_lockedItemsList, experienceEventsContainer, levelGate);
            _coffeeBreakMiniGame.Initialize(experienceEventsContainer, _balloonPopEventRule);

            _cursorPresenter.Initialize(null, _movementRoot.MovementRouter, null, null);
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
                ? new MobileInput(_movementJoystick, _mobileInteractiveArea, StepChangeDistanceCamera, _jump, null, null, _zoom, null)
                : new StandaloneInput(StepChangeDistanceCamera);
        }

#if UNITY_EDITOR
        [ProButton]
        public void FindAllSpawnPoints()
        {
            _collectableItemsSpawnPoints = null;
            _collectableItemsSpawnPoints = FindObjectsOfType<CollectableItemSpawnPoint>();
        }
#endif
    }
}
