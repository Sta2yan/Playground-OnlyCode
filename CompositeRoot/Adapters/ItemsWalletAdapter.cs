using System;
using Agava.Inventory;
using Agava.Shop;

namespace Agava.Playground3D.CompositeRoot
{
    public class ItemsWalletAdapter : IItemsWallet
    {
        private readonly IInventory _inventory;
        private readonly IInventoryView _inventoryView;

        public ItemsWalletAdapter(IInventory inventory, IInventoryView inventoryView)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
        }
        
        public bool CanSpend(string id, int count)
        {
            return _inventory.HasItemsWith(id, count);
        }

        public void Spend(string id, int count)
        {
            if (CanSpend(id, count) == false)
                throw new InvalidOperationException();

            _inventory.Remove(new InventoryItem(id), count);
            _inventory.Visualize(_inventoryView);
        }
    }
}
