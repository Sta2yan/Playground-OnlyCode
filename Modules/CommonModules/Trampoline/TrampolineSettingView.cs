using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Trampoline
{
    internal class TrampolineSettingView : MonoBehaviour
    {
        private const float StepChangeForcePush = 5f;
        
        [SerializeField] private TrampolineTrigger _trampoline;
        [SerializeField] private GameObject _settingPanel;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _plusForcePushButton;
        [SerializeField] private Button _minusForcePushButton;
        [SerializeField] private TMP_Text _forcePush;
        [SerializeField] private List<Button> _closeButtons;

        private void Awake()
        {
            _settingButton.gameObject.SetActive(false);
            _settingPanel.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TrampolineTarget _))
            {
                _settingButton.gameObject.SetActive(true);
                _settingButton.onClick.AddListener(OnSettingButtonClick);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TrampolineTarget _))
            {
                _settingButton.gameObject.SetActive(false);
                _settingButton.onClick.RemoveListener(OnSettingButtonClick);
            }
        }

        private void OnSettingButtonClick()
        {
            _settingPanel.SetActive(true);
            UpdateJumpForceText();
            
            _plusForcePushButton.onClick.AddListener(OnPlusForcePushButtonClick);
            _minusForcePushButton.onClick.AddListener(OnMinusForcePushButtonClick);

            foreach (var button in _closeButtons) 
                button.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            _settingPanel.SetActive(false);
            
            _plusForcePushButton.onClick.RemoveListener(OnPlusForcePushButtonClick);
            _minusForcePushButton.onClick.RemoveListener(OnMinusForcePushButtonClick);
            
            foreach (var button in _closeButtons) 
                button.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnPlusForcePushButtonClick()
        {
            _trampoline.ChangeJumpForce(_trampoline.CurrentJumpForce + StepChangeForcePush);
            UpdateJumpForceText();
        }

        private void OnMinusForcePushButtonClick()
        {
            if (_trampoline.CurrentJumpForce - StepChangeForcePush < 0)
                return;
            
            _trampoline.ChangeJumpForce(_trampoline.CurrentJumpForce - StepChangeForcePush);
            UpdateJumpForceText();
        }

        private void UpdateJumpForceText()
        {
            _forcePush.text = _trampoline.CurrentJumpForce.ToString();
        }
    }
}
