using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Shop
{
    internal class ShopView : MonoBehaviour, IShopView
    {
        private readonly List<CatalogView> _catalogViewInstances = new();

        [SerializeField] private CatalogView _catalogViewTemplate;
        [SerializeField] private Transform _catalogViewContent;
        [SerializeField] private VerticalLayoutGroup _layoutGroup;

        public void Render(ICatalog[] catalogs)
        {
            gameObject.SetActive(true);

            foreach (var instance in _catalogViewInstances)
                Destroy(instance.gameObject);

            _catalogViewInstances.Clear();

            foreach (var catalog in catalogs)
            {
                var catalogView = Instantiate(_catalogViewTemplate, _catalogViewContent);
                catalogView.Render(catalog);

                _catalogViewInstances.Add(catalogView);
            }

            StartCoroutine(RecalculatingHeightCatalog());
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator RecalculatingHeightCatalog()
        {
            yield return null;

            foreach (var catalogViewInstance in _catalogViewInstances)
                catalogViewInstance.RecalculateHeight();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroup.transform as RectTransform);
        }
    }
}
