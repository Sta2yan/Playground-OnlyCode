using Agava.AdditionalPredefinedMethods;
using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Playground3D.Shop
{
    public class SandboxShopRouter : IGameLoop, IShopRouter
    {
        private readonly IInput _input;
        private readonly IInventory _inventory;
        private readonly IInventoryView _inventoryView;
        private readonly ItemsList _itemsList;
        private readonly Agava.Shop.Shop _shop;

        public SandboxShopRouter(IInput input, Agava.Shop.Shop shop, IInventory inventory, IInventoryView inventoryView, ItemsList itemsList)
        {
            _input = input;
            _inventoryView = inventoryView;
            _inventory = inventory;
            _itemsList = itemsList;
            _shop = shop;
        }

        public void Update(float _)
        {
            if (_input.InteractiveShop() && _shop.Opened == false)
            {
                _shop.Open();
            }
            else if (_input.InteractiveShop() && _shop.Opened)
            {
                _shop.Close();
            }

            while (_shop.HasPurchasedProducts)
            {
                var purchasedItem = _shop.PopPurchasedProduct();
                
                if (_itemsList.TryGetItemById(purchasedItem.Id, out var item))
                {
                    AddToInventory(new InventoryItem(item.Id, item.MaxStack, item.Icon), purchasedItem.Count);
                }
            }

            foreach (string id in _shop.SingleProductIds)
            {
                if (_inventory.HasItemsWith(id, 1) == false)
                {
                    _shop.ResetProduct(id);
                }
            }
        }

        private void AddToInventory(InventoryItem item, int count)
        {
            for (var i = 0; i < count; i++)
                _inventory.Add(item);

            _inventory.Visualize(_inventoryView);
        }
    }
}
