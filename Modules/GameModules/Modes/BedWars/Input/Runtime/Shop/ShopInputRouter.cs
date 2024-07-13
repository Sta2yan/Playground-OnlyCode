using System.Linq;
using Agava.AdditionalPredefinedMethods;
using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Items;
using Agava.Shop;

namespace Agava.Playground3D.Input
{
    internal class ShopInputRouter : IGameLoop
    {
        private readonly IInput _input;
        private readonly Inventory.Inventory _inventory;
        private readonly InventoryView _inventoryView;
        private readonly ShopTrigger[] _shopTriggers;
        private readonly ItemsList _itemsList;
        private readonly Agava.Shop.Shop _shop;

        public ShopInputRouter(IInput input, Agava.Shop.Shop shop, ShopTrigger[] shopTriggers, Inventory.Inventory inventory, InventoryView inventoryView, ItemsList itemsList)
        {
            _input = input;
            _inventoryView = inventoryView;
            _shopTriggers = shopTriggers;
            _inventory = inventory;
            _itemsList = itemsList;
            _shop = shop;
        }

        private bool ShopTriggerEntered => _shopTriggers.Any(trigger => trigger.Entered);

        public void Update(float _)
        {
            if (_input.InteractiveShop() && _shop.Opened == false)
            {
                if (ShopTriggerEntered)
                    _shop.Open();
            }
            else if (_input.InteractiveShop() && _shop.Opened)
            {
                if (ShopTriggerEntered)
                    _shop.Close();
            }

            while (_shop.HasPurchasedProducts)
            {
                var purchasedItem = _shop.PopPurchasedProduct();

                if (_itemsList.TryGetItemById(purchasedItem.Id, out var item))
                    AddToInventory(new InventoryItem(item.Id, item.MaxStack, item.Icon), purchasedItem.Count);
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
