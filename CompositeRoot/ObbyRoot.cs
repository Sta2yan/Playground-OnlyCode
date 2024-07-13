using Agava.CheckPoints;
using Agava.ExperienceSystem;
using Agava.Input;
using Agava.Playground3D.Input;
using Agava.Playground3D.MovementFactories;
using Agava.Playground3D.Obby.Finish;
using Agava.Tests;
using UnityEngine;
#if YANDEX_GAMES
using Agava.WebUtility;
#endif

namespace Agava.Playground3D.CompositeRoot
{
    public class ObbyRoot : CompositeRoot
    {
        private const float StepChangeDistanceCamera = .5f;

        [SerializeField] private MovementRoot _movementRoot;
        [SerializeField] private ExperienceSystemRoot _experienceSystemRoot;
        [SerializeField] private CursorPresenter _cursorPresenter;
        [SerializeField] private PointsContainer _pointsContainer;
        [SerializeField] private FinishTrigger _finishTrigger;

        [Header("Experience system")]
        [SerializeField] private LockedItemsList _lockedItemsList;
        [SerializeField] private ExperienceEventRule _pointTriggeredEventRule;
        [SerializeField] private ExperienceEventRule _playerWonEventRule;

        [Header("MobileInput")]
        [SerializeField] private GameObject _mobileUi;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private MobileInteractiveArea _mobileInteractiveArea;
        [SerializeField] private ButtonTrackingArea _sprint;
        [SerializeField] private ButtonTrackingArea _jump;

        [Header("Editor")]
        [SerializeField] private GlobalConfigStorage _config;

        private IInput _input;

        public override void Compose()
        {
            _input = CreateTargetInput();

            _movementRoot.Initialize(new ObbyMovementRouterFactory(_input));
            _cursorPresenter.Initialize(null, _movementRoot.MovementRouter, null, null);

            ExperienceEventsContainer experienceEventsContainer = new ExperienceEventsContainer();

            _pointsContainer.Initialize(experienceEventsContainer, _pointTriggeredEventRule);
            _finishTrigger.Initialize(experienceEventsContainer, _playerWonEventRule);
            _experienceSystemRoot.Initialize(_lockedItemsList, experienceEventsContainer);
        }

        private IInput CreateTargetInput()
        {
            bool mobileInput;

#if YANDEX_GAMES && !UNITY_EDITOR
            mobileInput = Device.IsMobile;
#endif

#if ANDROID_BUILD && !UNITY_EDITOR
            mobileInput = true;
#endif

#if UNITY_EDITOR
            mobileInput = _config.MobileInput;
#endif

            _mobileUi.gameObject.SetActive(mobileInput);

            return mobileInput
                ? new MobileInput(_movementJoystick, _mobileInteractiveArea, StepChangeDistanceCamera, _jump, _sprint, null, null, null)
                : new StandaloneInput(StepChangeDistanceCamera);
        }
    }
}
