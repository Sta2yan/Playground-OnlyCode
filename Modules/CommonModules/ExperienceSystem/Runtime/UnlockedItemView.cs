using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.ExperienceSystem
{
    public class UnlockedItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _description;

        public void Render(LockedItem item)
        {
            _icon.sprite = item.UnlockedIcon;
            _description.text = LeanLocalization.GetTranslationText(item.Description, item.Description);
        }
    }
}
