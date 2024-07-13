namespace Agava.Inventory
{
    internal class InventorySlot : ISlot
    {
        private readonly InventoryItem _nullableItem = new InventoryItem("", 0, null);

        private InventoryItem _currentItem;
        private int _currentItemsCount;

        public InventorySlot()
        {
            _currentItem = _nullableItem;
        }

        public int ItemsCount => _currentItemsCount;
        public InventoryItem CurrentItem => _currentItem;

        public bool Add(InventoryItem inventoryItem, int count = 1)
        {
            if (inventoryItem.Equals(_nullableItem))
                return false;

            if (_currentItemsCount > 0 && _currentItem.Equals(inventoryItem) == false)
                return false;

            if (HasFreeSpaceFor(count) == false)
                return false;

            _currentItem = inventoryItem;
            _currentItemsCount += count;

            return true;
        }

        public bool HasFreeSpaceFor(int count)
        {
            return _currentItem.Equals(_nullableItem) || _currentItemsCount + count <= _currentItem.MaxCount;
        }

        public void Remove(int count)
        {
            if (_currentItemsCount < count)
                throw new System.ArgumentOutOfRangeException(_currentItemsCount + "<" + count);

            _currentItemsCount -= count;

            if (_currentItemsCount == 0)
                _currentItem = _nullableItem;
        }
    }
}