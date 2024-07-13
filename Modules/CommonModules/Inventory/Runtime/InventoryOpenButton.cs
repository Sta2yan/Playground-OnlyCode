using UnityEngine;
using System.Linq;

namespace Agava.Inventory
{
    public class InventoryOpenButton : MonoBehaviour 
    {
        [SerializeField] private SlotView[] _inventorySlots;
        
        private IInventory _inventory;

        public void Initialize(IInventory inventory)
        {
            _inventory = inventory;
        }

        public bool TryGetFirstFreeInventorySlot(out SlotView slotView)
        {
            int minIndex = _inventorySlots.Min(slot => slot.Index);
            slotView = null;

            if (_inventory.TryGetFirstFreeInventorySlot(minIndex, out int slotIndex)) 
            {
                slotView = _inventorySlots.FirstOrDefault(slotView => slotView.Index == slotIndex);
            }

            return slotView != null;
        }
    }
}
