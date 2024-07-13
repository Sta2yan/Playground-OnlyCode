namespace Agava.Inventory
{
    public interface IInventory
    {
        int SelectedSlotIndex { get; }
        int ItemsCountInSelectedSlot { get; }

        string ItemIdBy(int slotIndex);
        void Select(int slotIndex);
        void Move(int indexFrom, int indexTo);
        bool Merge(int indexFrom, int indexTo);
        void Add(InventoryItem inventoryItem);
        void Remove(InventoryItem item, int count);
        bool HasItemsWith(string itemId, int count);
        bool TryRemove(int index, int count);
        void Visualize(IInventoryView inventoryView);
        bool CanRemove(int index, int count);
        bool TryGetFirstFreeInventorySlot(int firstIndex, out int slotIndex);
    }
}
