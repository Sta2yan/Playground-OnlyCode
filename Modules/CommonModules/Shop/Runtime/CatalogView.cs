using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Agava.Shop
{
    internal class CatalogView : MonoBehaviour, ICatalogView
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Transform _viewContent;
        [SerializeField] private ProductView _productViewTemplate;

        public void Render(ICatalog catalog)
        {
            _name.text = LeanLocalization.GetTranslationText(catalog.Name, catalog.Name);

            foreach (var product in catalog.Products)
            {
#if !UNITY_EDITOR
                if (product.DebugProduct)
                    continue;
#endif

                var productView = Instantiate(_productViewTemplate, _viewContent.transform);
                productView.Render(product);
            }
        }

        internal void RecalculateHeight()
        {
            float childrenHeight = 0f;
            int childCount = transform.childCount;

            for (int i = 0; i < childCount; i++)
                childrenHeight += (transform.GetChild(i) as RectTransform).rect.height;

            var rectTransform = transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, childrenHeight);
        }
    }
}
