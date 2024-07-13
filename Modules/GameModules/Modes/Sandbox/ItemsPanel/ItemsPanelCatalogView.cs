using Agava.Shop;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Sandbox.ItemsPanel
{
    internal class ItemsPanelCatalogView : MonoBehaviour, ICatalogView
    {
        [SerializeField] private Transform _viewContent;
        [SerializeField] private ProductView _productViewTemplate;

        private ICatalog _lastCatalog;
        private List<ProductView> _views = new();

        public void Render(ICatalog catalog)
        {
            ClearView();

            foreach (var product in catalog.Products)
            {
#if !UNITY_EDITOR
                if (product.DebugProduct)
                    continue;
#endif

                var productView = Instantiate(_productViewTemplate, _viewContent.transform);
                _views.Add(productView);
                productView.Render(product);
            }

            _lastCatalog = catalog;
        }

        public void RenderLastCatalog()
        {
            if (_lastCatalog != null)
                Render(_lastCatalog);
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

        private void ClearView()
        {
            foreach (ProductView view in _views)
                Destroy(view.gameObject);

            _views.Clear();
        }
    }
}
