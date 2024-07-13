using System.Collections;
using Agava.ExperienceSystem;
using Agava.Input;
using Agava.Playground3D.CoffeeBreak;
using Agava.Playground3D.Input;
using Agava.Playground3D.MainMenu;
using Agava.Playground3D.MovementFactories;
using Agava.Tests;
using Agava.WebUtility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using System.Collections.Generic;
using Agava.Combat;
using Agava.Playground3D.CombatRouterFactories;

namespace Agava.Playground3D.CompositeRoot
{
    public class MainMenuRoot : CompositeRoot
    {
        private const float StepChangeDistanceCamera = .5f;

        [Header("Roots")]
        [SerializeField] private MovementRoot _movementRoot;
        [SerializeField] private ExperienceSystemRoot _experienceSystemRoot;
        [SerializeField] private MainMenuBotRoot _botRoot;
        [SerializeField] private CombatRoot _combatRoot;
        [SerializeField] private IAPRoot _iAPRoot;

        [Header("Common")]
        [SerializeField] private CursorPresenter _cursorPresenter;
        [SerializeField] private ModelRotation _modelRotation;
        [SerializeField] private Transform _portalSpawnPosition;
        [SerializeField] private GameObject _portalContainerPrefab;
        [SerializeField] private CoffeeBreakMiniGame _coffeeBreakMiniGame;
        [SerializeField] private Sword _sword;

        [Header("Experience system")]
        [SerializeField] private LockedItemsList _lockedItemsList;
        [SerializeField] private ExperienceEventRule _balloonPopEventRule;
        [SerializeField] private int _experienceClickerZone;

        [Header("MobileInput")]
        [SerializeField] private MobileInteractiveArea _mobileInteractiveArea;
        [SerializeField] private GameObject _mobileUi;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private ButtonTrackingArea _attack;
        [SerializeField] private ButtonTrackingArea _jump;

        [Header("Editor")]
        [SerializeField] private GlobalConfigStorage _config;

        private bool _mobile;
        private GameObject _portals;

        public override void Compose()
        {
            _mobile = IsMobileInput();
            IInput input = CreateTargetInput();

            ExperienceEventsContainer experienceEventsContainer = new ExperienceEventsContainer();
            LevelGate levelGate = new LevelGate(_lockedItemsList, _config.AllContentUnlocked);

            _modelRotation.Initialize(input);
            _movementRoot.Initialize(new MainMenuMovementRouterFactory(input));
            _experienceSystemRoot.Initialize(_lockedItemsList, experienceEventsContainer, levelGate);
            _cursorPresenter.Initialize(null, _movementRoot.MovementRouter, null, null);
            _coffeeBreakMiniGame.Initialize(experienceEventsContainer, _balloonPopEventRule);
            _combatRoot.Initialize(null, null, _mobile, new MenuCombatRouterFactory(input, null, _attack.gameObject, null, experienceEventsContainer, _sword, new ExperienceEvent(_experienceClickerZone)));

#if YANDEX_GAMES && !UNITY_EDITOR
            StartCoroutine(LoadPortals());
#else
            _portals = Instantiate(_portalContainerPrefab, _portalSpawnPosition);
#endif

            StartCoroutine(InitializePortals(levelGate));
        }

        private IEnumerator LoadPortals()
        {
            var portalsLoadOperation = Addressables.LoadAssetAsync<GameObject>("Portals");

            yield return portalsLoadOperation;

            _portals = Instantiate(portalsLoadOperation.Result, _portalSpawnPosition);
        }

        private IEnumerator InitializePortals(LevelGate levelGate)
        {
            yield return new WaitUntil(() => _portals != null);

            LevelGatePortalContainer[] portalContainers = _portals.GetComponentsInChildren<LevelGatePortalContainer>(true);
            var targetSpots = _portals.GetComponentsInChildren<PortalBotTargetSpot>();

            _iAPRoot.InitializePortalPurchase(portalContainers);

            foreach (var container in portalContainers)
            {
                levelGate.AddContainer(container);
            }

            AstarPath.active.Scan();
            _botRoot.Initialize(targetSpots);
        }

        private bool IsMobileInput()
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
                ? new MobileInput(_movementJoystick, _mobileInteractiveArea, StepChangeDistanceCamera, _jump, null, null, null, null)
                : new StandaloneInput(StepChangeDistanceCamera);
        }
    }
}
