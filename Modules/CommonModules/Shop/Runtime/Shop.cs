using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Agava.ExperienceSystem;

namespace Agava.Shop
{
    public class Shop : MonoBehaviour
    {
        private readonly Stack<PurchasedProduct> _purchasedProducts = new();

        [SerializeField] private MonoBehaviour _shopViewObject;
        [SerializeField] private SelectedProduct _selectedProduct;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private List<Button> _closeButtons;
        [SerializeField] private Catalog[] _catalogs;

        private IItemsWallet _itemsWallet;

        private IShopView _shopView => (IShopView)_shopViewObject;

        public bool HasPurchasedProducts => _purchasedProducts.Count != 0;
        public bool Opened { get; private set; }
        public List<string> SingleProductIds { get; private set; } = new();

        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(OnPurchaseButtonClick);

            foreach (var closeButton in _closeButtons)
                closeButton.onClick.AddListener(Close);

            _selectedProduct.Changed += OnSelectedProductChanged;
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.RemoveListener(OnPurchaseButtonClick);

            foreach (var closeButton in _closeButtons)
                closeButton.onClick.RemoveListener(Close);

            _selectedProduct.Changed -= OnSelectedProductChanged;
        }

        private void OnValidate()
        {
            if (_shopViewObject && _shopViewObject is not IShopView)
            {
                Debug.LogError(nameof(_shopViewObject) + " needs to implement " + nameof(IShopView));
                _shopViewObject = null;
            }
        }

        public void Initialize(IItemsWallet itemsWallet, LevelGate levelGate = null)
        {
            if (_itemsWallet != null)
                throw new InvalidOperationException("Already Initialized");

            _itemsWallet = itemsWallet;
            _shopView.Close();

            foreach (var catalog in _catalogs)
            {
                catalog.ResetProducts();

                foreach (IProduct product in catalog.Products)
                {
                    if (product.Single)
                        SingleProductIds.Add(product.Id);
                }

                levelGate?.AddContainer(catalog);
            }
        }

        public void Open()
        {
            _shopView.Render(_catalogs);

            if (_catalogs.Length > 0)
                _selectedProduct.Select(_catalogs[0].Products[0]);

            Opened = true;
        }

        public void Close()
        {
            _shopView.Close();
            Opened = false;
        }

        public PurchasedProduct PopPurchasedProduct()
        {
            if (HasPurchasedProducts == false)
                throw new InvalidOperationException();

            return _purchasedProducts.Pop();
        }

        public void ResetProduct(string itemId)
        {
            IProduct[] products;

            foreach (var catalog in _catalogs)
            {
                products = catalog.Products;

                foreach (var product in products)
                {
                    if (product.Id == itemId)
                    {
                        product.Reset();
                        return;
                    }
                }
            }
        }

        private void OnSelectedProductChanged(IProduct currentSelectedProduct)
        {
            UpdateSelectedPriductRenderd();

            if (Opened == true && currentSelectedProduct == _selectedProduct.PreviousValue)
                OnPurchaseButtonClick();
        }

        private void UpdateSelectedPriductRenderd()
        {
            if (CanBuySelectedProduct() == false)
                _selectedProduct.RenderUnavailable();
        }

        private void OnPurchaseButtonClick()
        {
            if (_itemsWallet == null)
                throw new InvalidOperationException("Need initialize");

            if (CanBuySelectedProduct() == false)
                return;

            _itemsWallet.Spend(_selectedProduct.Value.PriceItemId, _selectedProduct.Value.PriceItemCount);

            var purchasedItem = new PurchasedProduct(_selectedProduct.Value.Id, _selectedProduct.Value.Count);
            _purchasedProducts.Push(purchasedItem);
            _selectedProduct.Value.Buy();

            UpdateSelectedPriductRenderd();
        }

        private bool CanBuySelectedProduct()
        {
            var selectedProduct = _selectedProduct.Value;

            if (selectedProduct == null)
                return false;

            return selectedProduct.CanBuy &&
                   _itemsWallet.CanSpend(selectedProduct.PriceItemId, selectedProduct.PriceItemCount);
        }
    }
}
