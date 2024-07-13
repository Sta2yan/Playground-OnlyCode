using Agava.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Playground3D.Input
{
    internal class InventorySlotInput : MonoBehaviour, IPointerDownHandler
    {
        private SlotView _slotView;
        private IInventory _inventory;
        private IInventoryView _inventoryView;

        public void Init(SlotView slotView, IInventory inventory, IInventoryView inventoryView)
        {
            _slotView = slotView;
            _inventory = inventory;
            _inventoryView = inventoryView;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _inventory.Select(_slotView.Index);
            _inventory.Visualize(_inventoryView);
        }
    }
}
