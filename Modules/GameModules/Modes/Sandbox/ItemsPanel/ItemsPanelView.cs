using UnityEngine;
using Agava.Shop;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

namespace Agava.Playground3D.Sandbox.ItemsPanel
{
    internal class ItemsPanelView : MonoBehaviour, IShopView
    {
        private readonly Dictionary<CatalogTabView, ItemsPanelCatalogView> _instances = new();

        [SerializeField] private ItemsPanelCatalogView _catalogViewTemplate;
        [SerializeField] private Transform _catalogViewContent;
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private Transform _tabsContent;
        [SerializeField] private CatalogTabView _catalogTabViewTemplate;

        private CatalogTabView _selectedTab;

        private void Update()
        {
            foreach (CatalogTabView tab in _instances.Keys)
            {
                if (tab.Selected && _selectedTab != tab)
                {
                    SelectTab(tab);
                }
            }
        }

        public void Render(ICatalog[] catalogs)
        {
            gameObject.SetActive(true);

            if (_instances.Count != 0)
            {
                foreach (ItemsPanelCatalogView catalog in _instances.Values)
                    catalog.RenderLastCatalog();

                return;
            }

            foreach (var instance in _instances)
            {
                Destroy(instance.Key.gameObject);
                Destroy(instance.Value.gameObject);
                _selectedTab = null;
            }

            _instances.Clear();

            foreach (var catalog in catalogs)
            {
                CatalogTabView tab = Instantiate(_catalogTabViewTemplate, _tabsContent);
                tab.Render(catalog.Icon);

                ItemsPanelCatalogView catalogView = Instantiate(_catalogViewTemplate, _catalogViewContent);
                catalogView.Render(catalog);

                _instances.Add(tab, catalogView);

                if (_selectedTab == null)
                {
                    SelectTab(tab);
                }
                else
                {
                    tab.Unselect();
                    catalogView.gameObject.SetActive(false);
                }
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

            foreach (var catalogViewInstance in _instances.Values)
                catalogViewInstance.RecalculateHeight();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroup.transform as RectTransform);
        }

        private void SelectTab(CatalogTabView tab)
        {
            if (_instances.ContainsKey(tab))
            {
                if (_selectedTab != null)
                {
                    if (_instances.ContainsKey(_selectedTab))
                    {
                        _selectedTab.Unselect();
                        _instances[_selectedTab].gameObject.SetActive(false);
                    }
                }

                tab.Select();
                _instances[tab].gameObject.SetActive(true);
                _selectedTab = tab;
            }
        }
    }
}