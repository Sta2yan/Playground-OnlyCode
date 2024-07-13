using System.Collections.Generic;
using Agava.Audio;
using Agava.Customization;
using Agava.Levels;
using Agava.Movement;
using Agava.Playground3D.Loading;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.MainMenu
{
    internal class Menu : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;
        [SerializeField] private CustomizationMenu _customizationMenu;
        [SerializeField] private CameraMovement _camera;
        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private GameObject _searchPanel;
        [SerializeField] private GameObject _modesPanel;
        [SerializeField] private float _loadDelay;
        [SerializeField] private Button _customizationButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _backToMenuButton;
        //[SerializeField] private ModelRotation _modelRotation;
        [SerializeField] private Button _closeSettingsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameObject _settingPanel;
        [SerializeField] private Slider _sensitivity;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private Toggle _audioToggle;
        [SerializeField] private GameObject _languageSelectPanel;

        [Header("Mode Buttons")]
        [SerializeField] private Button _bedWarsButton;
        [SerializeField] private Button _onlyUpButton;
        [SerializeField] private Button _sandboxButton;
        [SerializeField] private Button _obbyButton;

        [Header("Tutorial Buttons")]
        [SerializeField] private Button _tutorialBedWarsButton;
        [SerializeField] private Button _tutorialOnlyUpButton;
        [SerializeField] private Button _tutorialSandboxButton;
        [SerializeField] private Button _tutorialObbyButton;

        [Header("Tutorial Description")]
        [SerializeField] private GameObject _tutorialDescriptionBedWars;
        [SerializeField] private GameObject _tutorialDescriptionOnlyUp;
        [SerializeField] private GameObject _tutorialDescriptionSandbox;
        [SerializeField] private GameObject _tutorialDescriptionObby;

        [Header("Group Buttons")]
        [SerializeField] private List<Button> _closeTutorialButtons;
        [SerializeField] private List<Button> _closeModesPanel;

        private bool _leaderboardButtonActive = false;

        private void Awake()
        {
            _backToMenuButton.gameObject.SetActive(false);
            _tutorialPanel.SetActive(false);
            _searchPanel.SetActive(false);
            _modesPanel.SetActive(false);
            //_modelRotation.gameObject.SetActive(true);

            _tutorialDescriptionBedWars.SetActive(false);
            _tutorialDescriptionSandbox.SetActive(false);
            _tutorialDescriptionOnlyUp.SetActive(false);
            _tutorialDescriptionObby.SetActive(false);

#if YANDEX_GAMES
            if (_languageSelectPanel != null)
            {
                _languageSelectPanel.SetActive(false);
            }
            
            _leaderboardButtonActive = true;
#endif

            _leaderboardButton.gameObject.SetActive(_leaderboardButtonActive && true);
        }

        private void OnEnable()
        {
            _tutorialBedWarsButton.onClick.AddListener(OnOpenTutorialButtonClick);
            _tutorialSandboxButton.onClick.AddListener(OnOpenTutorialButtonClick);
            _tutorialOnlyUpButton.onClick.AddListener(OnOpenTutorialButtonClick);
            _tutorialObbyButton.onClick.AddListener(OnOpenTutorialButtonClick);

            _tutorialBedWarsButton.onClick.AddListener(OnOpenTutorialBedWarsButtonClick);
            _tutorialSandboxButton.onClick.AddListener(OnOpenTutorialSandboxButtonClick);
            _tutorialOnlyUpButton.onClick.AddListener(OnOpenTutorialOnlyUpButtonClick);
            _tutorialObbyButton.onClick.AddListener(OnOpenTutorialObbyButtonClick);

            _bedWarsButton.onClick.AddListener(OnBedWarsButtonClick);
            _sandboxButton.onClick.AddListener(OnSandboxButtonClick);
            _onlyUpButton.onClick.AddListener(OnOnlyUpButtonClick);
            _obbyButton.onClick.AddListener(OnObbyButtonClick);

            _customizationButton.onClick.AddListener(OnCustomizationButtonClick);
            _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);

            foreach (var button in _closeTutorialButtons)
                button.onClick.AddListener(OnCloseTutorialButtonClick);

            foreach (var button in _closeModesPanel)
                button.onClick.AddListener(OnCloseModesPanelClick);

            _settingsButton.onClick.AddListener(OnSettingButtonClick);
            _closeSettingsButton.onClick.AddListener(OnCloseButtonClick);
            _audioToggle.onValueChanged.AddListener(OnAudioSettingsChanged);
            _sensitivity.onValueChanged.AddListener(OnSensitivityChanged);
        }

        private void OnDisable()
        {
            _tutorialBedWarsButton.onClick.RemoveListener(OnOpenTutorialButtonClick);
            _tutorialSandboxButton.onClick.RemoveListener(OnOpenTutorialButtonClick);
            _tutorialOnlyUpButton.onClick.RemoveListener(OnOpenTutorialButtonClick);
            _tutorialObbyButton.onClick.RemoveListener(OnOpenTutorialButtonClick);

            _tutorialBedWarsButton.onClick.RemoveListener(OnOpenTutorialBedWarsButtonClick);
            _tutorialSandboxButton.onClick.RemoveListener(OnOpenTutorialSandboxButtonClick);
            _tutorialOnlyUpButton.onClick.RemoveListener(OnOpenTutorialOnlyUpButtonClick);
            _tutorialObbyButton.onClick.RemoveListener(OnOpenTutorialObbyButtonClick);

            _bedWarsButton.onClick.RemoveListener(OnBedWarsButtonClick);
            _sandboxButton.onClick.RemoveListener(OnSandboxButtonClick);
            _onlyUpButton.onClick.RemoveListener(OnOnlyUpButtonClick);
            _obbyButton.onClick.RemoveListener(OnObbyButtonClick);

            _customizationButton.onClick.RemoveListener(OnCustomizationButtonClick);
            _backToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);

            foreach (var button in _closeTutorialButtons)
                button.onClick.RemoveListener(OnCloseTutorialButtonClick);

            foreach (var button in _closeModesPanel)
                button.onClick.RemoveListener(OnCloseModesPanelClick);

            _settingsButton.onClick.RemoveListener(OnSettingButtonClick);
            _closeSettingsButton.onClick.RemoveListener(OnCloseButtonClick);
            _audioToggle.onValueChanged.RemoveListener(OnAudioSettingsChanged);
            _sensitivity.onValueChanged.RemoveListener(OnSensitivityChanged);
        }

        private void Update()
        {
            _audioToggle.isOn = EnableAudio.Enabled;
        }

        private void OnAudioSettingsChanged(bool audioOn)
        {
            if (audioOn)
            {
                EnableAudio.Enable();
            }
            else
            {
                EnableAudio.Disable();
            }
        }

        private void OnSensitivityChanged(float value)
        {
            _cameraMovement.ChangeSensitivity(value);
        }

        private void OnSettingButtonClick()
        {
            _settingPanel.SetActive(true);
            _settingsButton.interactable = false;
            Time.timeScale = 0;
        }

        private void OnCloseButtonClick()
        {
            _settingPanel.SetActive(false);
            _settingsButton.interactable = true;
            Time.timeScale = 1;
        }

        private void OnBedWarsButtonClick()
        {
            _searchPanel.SetActive(true);
            Invoke(nameof(StartBedWars), _loadDelay);
        }

        private void OnOnlyUpButtonClick()
        {
            Invoke(nameof(StartOnlyUp), 0f);
        }

        private void OnSandboxButtonClick()
        {
            Invoke(nameof(StartSandbox), 0f);
        }

        private void OnObbyButtonClick()
        {
            Invoke(nameof(StartObby), 0f);
        }

        private void OnPlayButtonClick()
        {
            _modesPanel.SetActive(true);
            EnableModelRotation(false);
        }

        private void OnCloseModesPanelClick()
        {
            _modesPanel.SetActive(false);

            _bedWarsButton.gameObject.SetActive(false);
            _sandboxButton.gameObject.SetActive(false);
            _onlyUpButton.gameObject.SetActive(false);
            _obbyButton.gameObject.SetActive(false);

            EnableModelRotation(true);
        }

        private void OnOpenTutorialButtonClick()
        {
            _tutorialPanel.SetActive(true);
        }

        private void OnOpenTutorialBedWarsButtonClick()
        {
            _tutorialDescriptionBedWars.SetActive(true);
        }

        private void OnOpenTutorialOnlyUpButtonClick()
        {
            _tutorialDescriptionOnlyUp.SetActive(true);
        }

        private void OnOpenTutorialSandboxButtonClick()
        {
            _tutorialDescriptionSandbox.SetActive(true);
        }

        private void OnOpenTutorialObbyButtonClick()
        {
            _tutorialDescriptionObby.SetActive(true);
        }

        private void OnCloseTutorialButtonClick()
        {
            _tutorialPanel.SetActive(false);

            _tutorialDescriptionBedWars.SetActive(false);
            _tutorialDescriptionSandbox.SetActive(false);
            _tutorialDescriptionOnlyUp.SetActive(false);
            _tutorialDescriptionObby.SetActive(false);
        }

        private void StartBedWars()
        {
            _levelList.LoadBedWars();
        }

        private void StartOnlyUp()
        {
            _levelList.LoadOnlyUp();
        }

        private void StartSandbox()
        {
            _levelList.LoadSandbox();
        }

        private void StartObby()
        {
            _levelList.LoadObby();
        }

        private void OnCustomizationButtonClick()
        {
            _customizationButton.gameObject.SetActive(false);
            _leaderboardButton?.gameObject.SetActive(_leaderboardButtonActive && false);

            _customizationMenu.gameObject.SetActive(true);
            _backToMenuButton.gameObject.SetActive(true);

            _cameraMovement.ChangeDistance(10f);

            _customizationMenu.Show();
            _settingsButton.gameObject.SetActive(false);
        }

        private void OnBackToMenuButtonClick()
        {
            _customizationButton.gameObject.SetActive(true);
            _leaderboardButton?.gameObject.SetActive(_leaderboardButtonActive && true);

            _customizationMenu.gameObject.SetActive(false);
            _backToMenuButton.gameObject.SetActive(false);

            _customizationMenu.Hide();
            _settingsButton.gameObject.SetActive(true);
        }

        private void EnableModelRotation(bool enable)
        {
            //_modelRotation.gameObject.SetActive(enable);
        }
    }
}
