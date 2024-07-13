using Agava.Combat;
using Agava.Inventory;
using Agava.Playground3D.Items;

namespace Agava.Playground3D.Input
{
    internal class Magazine : IMagazine
    {
        private const int ShotCount = 1;

        private readonly IInventory _inventory;
        private readonly IInventoryView _inventoryView;
        private readonly InventoryItem _bulletInventoryItem;

        public Magazine(BulletItem bullet, IInventory inventory, IInventoryView inventoryView)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
            _bulletInventoryItem = new InventoryItem(bullet.Id);
        }

        public bool Has()
        {
            return _inventory.HasItemsWith(_bulletInventoryItem.Id, ShotCount);
        }

        public void Remove()
        {
            _inventory.Remove(_bulletInventoryItem, ShotCount);
            _inventory.Visualize(_inventoryView);
        }
    }

    internal class InfinityMagazine : IMagazine
    {
        public bool Has()
        {
            return true;
        }

        public void Remove()
        {
            
        }
    }
}
