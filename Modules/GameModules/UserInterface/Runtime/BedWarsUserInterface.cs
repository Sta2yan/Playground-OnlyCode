using System.Collections.Generic;
using Agava.Combat;
using Agava.Levels;
using Agava.Movement;
using Agava.Playground3D.BedWars.Combat;
using Agava.Playground3D.Loading;
using UnityEngine;
using UnityEngine.UI;
using Agava.Audio;

namespace Agava.Playground3D.UserInterface
{
    public class BedWarsUserInterface : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private CombatCharacter _player;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private GameObject _returnMenuPanel;
        [SerializeField] private Button _openReturnMenuButton;
        [SerializeField] private Button _closeMenuReturnButton;
        [SerializeField] private List<Button> _menuButtons;
        [SerializeField] private Slider _sensitivity;
        [SerializeField] private List<Button> _closeSettingButtons;
        [SerializeField] private Toggle _audioToggle;

        private float _currentDelay;
        private float _maximumDelay;
        private IBedWarsTeamList _teamList;

        private void Awake()
        {
            _settingsPanel.SetActive(false);
            _losePanel.Hide();
            _audioToggle.isOn = EnableAudio.Enabled;
        }

        private void OnEnable()
        {
            _sensitivity.value = _cameraMovement.Sensitivity;

            _sensitivity.onValueChanged.AddListener(OnSensitivityChanged);
            _settingsButton.onClick.AddListener(OnSettingButtonClick);
            _newGameButton.onClick.AddListener(OnRestartButtonClick);
            _openReturnMenuButton.onClick.AddListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.AddListener(OnCloseReturnMenuButtonClick);

            foreach (var closeSettingButton in _closeSettingButtons)
                closeSettingButton.onClick.AddListener(OnCloseSettingButtonClick);

            foreach (var menuButton in _menuButtons)
                menuButton.onClick.AddListener(OnMenuButtonClick);

            _audioToggle.onValueChanged.AddListener(OnAudioSettingsChanged);
        }

        private void Update()
        {
            if (_winPanel.activeSelf == false && _teamList.PlayerWin)
            {
                _winPanel.SetActive(true);
                return;
            }

            if (_player.Alive)
            {
                _losePanel.Hide();
                _currentDelay = _maximumDelay;
                return;
            }

            _currentDelay -= Time.deltaTime;
            _losePanel.Show();
            _losePanel.Visualize(_currentDelay);
        }

        private void OnDisable()
        {
            _sensitivity.onValueChanged.RemoveListener(OnSensitivityChanged);
            _settingsButton.onClick.RemoveListener(OnSettingButtonClick);
            _newGameButton.onClick.RemoveListener(OnRestartButtonClick);
            _openReturnMenuButton.onClick.RemoveListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.RemoveListener(OnCloseReturnMenuButtonClick);

            foreach (var closeSettingButton in _closeSettingButtons)
                closeSettingButton.onClick.RemoveListener(OnCloseSettingButtonClick);

            foreach (var menuButton in _menuButtons)
                menuButton.onClick.RemoveListener(OnMenuButtonClick);

            _audioToggle.onValueChanged.RemoveListener(OnAudioSettingsChanged);
        }

        public void Initialize(float delayToRespawn, IBedWarsTeamList teamList)
        {
            _maximumDelay = delayToRespawn;
            _teamList = teamList;
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

        private void OnCloseSettingButtonClick()
        {
            _settingsPanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void OnSettingButtonClick()
        {
            _settingsPanel.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnRestartButtonClick()
        {
            _levelList.LoadBedWars();
        }

        private void OnMenuButtonClick()
        {
            _levelList.LoadMainMenu();
            Time.timeScale = 1;
        }

        private void OnOpenReturnMenuButtonClick()
        {
            _returnMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnCloseReturnMenuButtonClick()
        {
            _returnMenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
