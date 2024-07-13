namespace Agava.Inventory
{
    public interface ISlot
    {
        int ItemsCount { get; }
        InventoryItem CurrentItem { get; }

        bool Add(InventoryItem inventoryItem, int count = 1);
        bool HasFreeSpaceFor(int count);
        void Remove(int count);
    }
}
