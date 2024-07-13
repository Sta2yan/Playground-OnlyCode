using Agava.ExperienceSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Shop
{
    public class ProductView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lockedIcon;

        public IProduct Product { get; private set; }

        public void Render(IProduct product)
        {
            Product = product;
            _icon.sprite = product.Icon;

            ILevelGateContent levelGateProduct = product as ILevelGateContent;

            if (levelGateProduct != null)
                _lockedIcon?.gameObject.SetActive(!levelGateProduct.Unlocked);
        }
    }
}
