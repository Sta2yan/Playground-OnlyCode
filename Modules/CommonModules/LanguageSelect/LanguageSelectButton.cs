using UnityEngine;
using Lean.Localization;
using UnityEngine.UI;

namespace Agava.LanguageSelect
{
    public class LanguageSelectButton : MonoBehaviour
    {
        [SerializeField] private LeanLanguage _language;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void Update()
        {
            _button.interactable = LeanLocalization.GetFirstCurrentLanguage() != _language.name;
        }

        private void OnButtonClick()
        {
            LeanLocalization.SetCurrentLanguageAll(_language.name);
        }
    }
}
