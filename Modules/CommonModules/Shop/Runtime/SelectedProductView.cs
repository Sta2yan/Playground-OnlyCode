using Agava.ExperienceSystem;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Shop
{
    internal class SelectedProductView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _priceIcon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Button _purchaseButton;

        [Header("Locked item")]
        [SerializeField] private TMP_Text _levelRequiredText;
        [SerializeField] private TMP_Text _levelRequiredAmountText;

        public void Render(IProduct product)
        {
            bool unlocked = true;

            _icon.sprite = product.Icon;
            string productName = LeanLocalization.GetTranslationText(product.TranslationId, product.TranslationId);

            if (product.DebugProduct)
                productName = string.Join(" ", "[DEBUG]", productName);

            _name.text = productName;

            if (_priceIcon != null)
                _priceIcon.sprite = product.PriceItemIcon;

            if (_count != null)
                _count.text = product.Count.ToString();

            if (_price != null)
                _price.text = product.PriceItemCount.ToString();

            _purchaseButton.interactable = true;

            ILevelGateContent levelGateContent = product as ILevelGateContent;

            if (levelGateContent != null)
                unlocked = levelGateContent.Unlocked;

            _levelRequiredText?.gameObject.SetActive(!unlocked);
            _levelRequiredAmountText?.gameObject.SetActive(!unlocked);

            if (_levelRequiredAmountText != null)
                _levelRequiredAmountText.text = levelGateContent.UnlockingLevel.ToString();
        }

        public void RenderUnavailable()
        {
            _purchaseButton.interactable = false;
        }
    }
}
