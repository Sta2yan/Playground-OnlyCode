using Agava.Audio;
using Agava.Levels;
using Agava.Movement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.UserInterface
{
    public class NewYearEventInterface : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;

        [Header("Main UI")]
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _openReturnMenuButton;

        [Header("Return to menu")]
        [SerializeField] private GameObject _returnMenuPanel;
        [SerializeField] private Button[] _menuButtons;
        [SerializeField] private Button _closeMenuReturnButton;

        [Header("Settings")]
        [SerializeField] private GameObject _settingPanel;
        [SerializeField] private Slider _sensitivity;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private List<Button> _settingCloseButtons;
        [SerializeField] private Toggle _audioToggle;

        [Header("Finish panel")]
        [SerializeField] private GameObject _finishPanel;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            _settingPanel.SetActive(false);
            _finishPanel.SetActive(false);

            _sensitivity.value = _cameraMovement.Sensitivity;
            _audioToggle.isOn = EnableAudio.Enabled;
        }

        private void OnEnable()
        {
            _sensitivity.onValueChanged.AddListener(OnSensitivityChanged);
            _settingButton.onClick.AddListener(OnSettingButtonClick);

            foreach (var menuButton in _menuButtons)
                menuButton.onClick.AddListener(OnMenuButtonClick);

            _openReturnMenuButton.onClick.AddListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.AddListener(OnCloseReturnMenuButtonClick);

            foreach (var button in _settingCloseButtons)
                button.onClick.AddListener(OnCloseSettingButtonClick);

            _restartButton.onClick.AddListener(OnRestartButtonClick);

            _audioToggle.onValueChanged.AddListener(OnAudioSettingsChanged);
        }

        private void OnDisable()
        {
            _sensitivity.onValueChanged.RemoveListener(OnSensitivityChanged);
            _settingButton.onClick.RemoveListener(OnSettingButtonClick);

            foreach (var menuButton in _menuButtons)
                menuButton.onClick.RemoveListener(OnMenuButtonClick);

            _openReturnMenuButton.onClick.RemoveListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.RemoveListener(OnCloseReturnMenuButtonClick);

            foreach (var button in _settingCloseButtons)
                button.onClick.RemoveListener(OnCloseSettingButtonClick);

            _restartButton.onClick.RemoveListener(OnRestartButtonClick);

            _audioToggle.onValueChanged.RemoveListener(OnAudioSettingsChanged);
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

        private void OnMenuButtonClick()
        {
            _levelList.LoadMainMenu();
        }

        private void OnSettingButtonClick()
        {
            _settingPanel.SetActive(true);
        }

        private void OnCloseSettingButtonClick()
        {
            _settingPanel.SetActive(false);
        }

        private void OnSensitivityChanged(float value)
        {
            _cameraMovement.ChangeSensitivity(value);
        }

        private void OnOpenReturnMenuButtonClick()
        {
            _returnMenuPanel.SetActive(true);
        }

        private void OnCloseReturnMenuButtonClick()
        {
            _returnMenuPanel.SetActive(false);
        }

        private void OnRestartButtonClick()
        {
            _levelList.LoadNewYearEvent();
        }
    }
}
