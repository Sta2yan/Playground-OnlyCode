using System;
using System.Collections.Generic;
using Agava.Levels;
using Agava.Movement;
using Agava.Playground3D.Loading;
using UnityEngine;
using UnityEngine.UI;
using Agava.Audio;

namespace Agava.Playground3D.UserInterface
{
    public class OnlyUpInterface : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameObject _returnMenuPanel;
        [SerializeField] private Button _openReturnMenuButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _closeMenuReturnButton;
        [SerializeField] private List<Button> _closeButtons;
        [SerializeField] private GameObject _settingPanel;
        [SerializeField] private Slider _sensitivity;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private Toggle _audioToggle;

        private void Awake()
        {
            _sensitivity.value = _cameraMovement.Sensitivity;
            _audioToggle.isOn = EnableAudio.Enabled;
        }

        private void OnEnable()
        {
            _sensitivity.onValueChanged.AddListener(OnSensitivityChanged);
            _settingsButton.onClick.AddListener(OnSettingButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _openReturnMenuButton.onClick.AddListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.AddListener(OnCloseReturnMenuButtonClick);

            foreach (var button in _closeButtons) 
                button.onClick.AddListener(OnCloseButtonClick);

            _audioToggle.onValueChanged.AddListener(OnAudioSettingsChanged);
        }

        private void OnDisable()
        {
            _sensitivity.onValueChanged.RemoveListener(OnSensitivityChanged);
            _settingsButton.onClick.RemoveListener(OnSettingButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
            _openReturnMenuButton.onClick.RemoveListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.RemoveListener(OnCloseReturnMenuButtonClick);

            foreach (var button in _closeButtons) 
                button.onClick.RemoveListener(OnCloseButtonClick);

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

        private void OnSettingButtonClick()
        {
            _settingPanel.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnCloseButtonClick()
        {
            _settingPanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void OnSensitivityChanged(float value)
        {
            _cameraMovement.ChangeSensitivity(value);
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
