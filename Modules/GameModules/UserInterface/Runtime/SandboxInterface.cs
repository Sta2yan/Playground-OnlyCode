using System.Collections.Generic;
using Agava.Levels;
using Agava.Movement;
using UnityEngine;
using UnityEngine.UI;
using Agava.Playground3D.Sandbox.Blocks;
using Agava.Playground3D.Loading;
using Agava.Audio;
using Agava.Playground3D.Sandbox.UserInterface;

namespace Agava.Playground3D.UserInterface
{
    public class SandboxInterface : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;
        [SerializeField] private GameObject _settingPanel;
        [SerializeField] private Shop.Shop _shop;
        [SerializeField] private Button _settingButton;
        [SerializeField] private GameObject _returnMenuPanel;
        [SerializeField] private Button _openReturnMenuButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _closeMenuReturnButton;
        [SerializeField] private Slider _sensitivity;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private List<Button> _settingCloseButtons;
        [SerializeField] private Button _clearGridButton;
        [SerializeField] private Button _saveGridButton;
        [SerializeField] private Button _openShotButton;
        [SerializeField] private GridClear _gridClear;
        [SerializeField] private GridUserData _gridUserData;
        [SerializeField] private GameObject _gridClearDialog;
        [SerializeField] private BotsCommunication _botsCommunication;
        [SerializeField] private Button _acceptClearGridButton;
        [SerializeField] private Button _rejectClearGridButton;
        [SerializeField] private Toggle _audioToggle;

        private void Awake()
        {
            _settingPanel.SetActive(false);
            _gridClearDialog.SetActive(false);
            _returnMenuPanel.SetActive(false);

            _sensitivity.value = _cameraMovement.Sensitivity;
            _audioToggle.isOn = EnableAudio.Enabled;
        }

        private void OnEnable()
        {
            _sensitivity.onValueChanged.AddListener(OnSensitivityChanged);
            _settingButton.onClick.AddListener(OnSettingButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _clearGridButton.onClick.AddListener(OnClearGridButtonClick);
            _saveGridButton.onClick.AddListener(OnSaveGridButtonClick);
            _openShotButton.onClick.AddListener(OnOpenShopButtonClick);
            _openReturnMenuButton.onClick.AddListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.AddListener(OnCloseReturnMenuButtonClick);

            foreach (var button in _settingCloseButtons)
                button.onClick.AddListener(OnCloseSettingButtonClick);

            _acceptClearGridButton.onClick.AddListener(OnAcceptClearGridButtonClick);
            _rejectClearGridButton.onClick.AddListener(OnRejectClearGridButtonClick);
            _audioToggle.onValueChanged.AddListener(OnAudioSettingsChanged);
        }

        private void OnDisable()
        {
            _sensitivity.onValueChanged.RemoveListener(OnSensitivityChanged);
            _settingButton.onClick.RemoveListener(OnSettingButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
            _clearGridButton.onClick.RemoveListener(OnClearGridButtonClick);
            _saveGridButton.onClick.RemoveListener(OnSaveGridButtonClick);
            _openShotButton.onClick.RemoveListener(OnOpenShopButtonClick);
            _openReturnMenuButton.onClick.RemoveListener(OnOpenReturnMenuButtonClick);
            _closeMenuReturnButton.onClick.RemoveListener(OnCloseReturnMenuButtonClick);

            foreach (var button in _settingCloseButtons)
                button.onClick.RemoveListener(OnCloseSettingButtonClick);

            _acceptClearGridButton.onClick.RemoveListener(OnAcceptClearGridButtonClick);
            _rejectClearGridButton.onClick.RemoveListener(OnRejectClearGridButtonClick);
            _audioToggle.onValueChanged.RemoveListener(OnAudioSettingsChanged);
        }

        private void Update()
        {
            _saveGridButton.interactable = _gridUserData.Available;
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
            Time.timeScale = 1;
        }

        private void OnSettingButtonClick()
        {
            _settingPanel.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnCloseSettingButtonClick()
        {
            _settingPanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void OnSensitivityChanged(float value)
        {
            _cameraMovement.ChangeSensitivity(value);
        }

        private void OnClearGridButtonClick()
        {
            _gridClearDialog.SetActive(true);
        }

        private void OnSaveGridButtonClick()
        {
            _gridUserData.Save();
        }

        private void OnOpenShopButtonClick()
        {
            if (_shop.Opened)
                _shop.Close();
            else
                _shop.Open();
        }

        private void OnAcceptClearGridButtonClick()
        {
            _botsCommunication.ClearBots();
            _gridClear.ClearGrid();
            _gridClearDialog.SetActive(false);
        }

        private void OnRejectClearGridButtonClick()
        {
            _gridClearDialog.SetActive(false);
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
