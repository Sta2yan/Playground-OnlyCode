using System;
using System.Linq;
using System.Security.Cryptography;

namespace Agava.Inventory
{
    public class Inventory : IInventory
    {
        private readonly ISlot[] _slots;

        private int _selectedSlotIndex = 0;

        public Inventory(int capacity)
        {
            _slots = new ISlot[capacity];

            for (int i = 0; i < _slots.Length; i++)
                _slots[i] = new InventorySlot();
        }

        public int SelectedSlotIndex => _selectedSlotIndex;
        public int ItemsCountInSelectedSlot => _slots[_selectedSlotIndex].ItemsCount;

        public void Select(int slotIndex)
        {
            _selectedSlotIndex = slotIndex;
        }

        public void Add(InventoryItem inventoryItem)
        {
            var itemAdded = false;

            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i].CurrentItem.Equals(inventoryItem))
                {
                    if (_slots[i].Add(inventoryItem))
                    {
                        itemAdded = true;
                        break;
                    }
                }
            }

            if (itemAdded)
                return;

            foreach (var slot in _slots)
            {
                if (slot.Add(inventoryItem))
                    return;
            }
        }

        public bool Merge(int indexFrom, int indexTo)
        {
            var slotFrom = _slots[indexFrom];
            var slotTo = _slots[indexTo];

            if (slotFrom == slotTo)
                return false;

            if (slotFrom.CurrentItem.Equals(slotTo.CurrentItem) == false)
                return false;

            if (slotFrom.ItemsCount == 0)
                return false;

            if (slotTo.HasFreeSpaceFor(slotFrom.ItemsCount) == false)
                return false;

            slotTo.Add(slotFrom.CurrentItem, slotFrom.ItemsCount);
            slotFrom.Remove(slotFrom.ItemsCount);

            return true;
        }

        public void Remove(InventoryItem item, int count)
        {
            if (HasItemsWith(item.Id, count) == false)
                throw new InvalidOperationException();

            int removedCount = 0;

            foreach (var slot in _slots)
            {
                if (slot.ItemsCount == 0 || slot.CurrentItem.Equals(item) == false)
                    continue;

                if (slot.ItemsCount < count - removedCount)
                {
                    removedCount += slot.ItemsCount;
                    slot.Remove(slot.ItemsCount);
                }
                else
                {
                    slot.Remove(count - removedCount);
                    return;
                }
            }
        }

        public bool TryRemove(int index, int count)
        {
            if (_slots[index].ItemsCount == 0)
                return false;

            _slots[index].Remove(count);
            return true;
        }

        public bool HasItemsWith(string itemId, int count)
        {
            var slotsWithTargetItem = _slots.Where(slot => slot.CurrentItem.Id == itemId).ToArray();

            if (slotsWithTargetItem.Any() == false)
                return count == 0;

            return slotsWithTargetItem.Sum(slot => slot.ItemsCount) >= count;
        }

        public bool CanRemove(int index, int count)
        {
            return _slots[index].ItemsCount >= count;
        }

        public void Move(int indexFrom, int indexTo)
        {
            if (indexFrom == indexTo)
                return;

            (_slots[indexFrom], _slots[indexTo]) = (_slots[indexTo], _slots[indexFrom]);
        }

        public string ItemIdBy(int slotIndex)
        {
            return _slots[slotIndex].CurrentItem.Id;
        }

        public void Visualize(IInventoryView inventoryView)
        {
            inventoryView.Render(_slots, _selectedSlotIndex);
        }

        public bool TryGetFirstFreeInventorySlot(int firstIndex, out int slotIndex)
        {
            ISlot slot = _slots.Skip(firstIndex).FirstOrDefault(slot => slot.ItemsCount == 0);

            if (slot == null)
            {
                slotIndex = -1;
                return false;
            }

            slotIndex = _slots.ToList().IndexOf(slot);
            return true;
        }
    }
}