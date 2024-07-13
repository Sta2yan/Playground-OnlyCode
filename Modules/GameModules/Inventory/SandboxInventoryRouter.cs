using Agava.AdditionalPredefinedMethods;
using Agava.DroppedItems;
using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Input;
using Agava.Playground3D.Items;
using System;

namespace Agava.Playground3D.Inventory
{
    public class SandboxInventoryRouter : IInventoryRouter, IGameLoop, IDisposable
    {
        private readonly IInput _input;
        private readonly IInventoryView _inventoryView;
        private readonly IInventory _inventory;
        private readonly TakenItemsList _takenItemsList;
        private readonly ItemsList _itemsList;
        private readonly Hand _hand;

        public SandboxInventoryRouter(IInput input, IInventory inventory, IInventoryView inventoryView, TakenItemsList takenItemsList, ItemsList itemsList, Hand hand)
        {
            _input = input;
            _inventory = inventory;
            _inventoryView = inventoryView;
            _takenItemsList = takenItemsList;
            _itemsList = itemsList;
            _hand = hand;

            _inventoryView.Moved += OnMoved;
            _inventoryView.Dropped += OnDropped;
        }

        public void Update(float _)
        {
            if (_input.InventoryItemSelected(out var index))
            {
                _inventory.Select(index);
                _inventory.Visualize(_inventoryView);
            }

            if (_input.DropItem())
                OnDropped(_inventory.SelectedSlotIndex);

            while (_takenItemsList.HasItems)
            {
                if (_itemsList.TryGetItemById(_takenItemsList.Pop(), out var item) == false)
                    continue;

                _inventory.Add(new InventoryItem(item.Id, item.MaxStack, item.Icon));
                _inventory.Visualize(_inventoryView);
            }

            if (_input.OpenInventory() && _inventoryView.Opened == false)
                _inventoryView.ActivateSlotsPanel();
            else if (_input.CloseInventory() && _inventoryView.Opened)
                _inventoryView.DisableSlotsPanel();

            if (_itemsList.TryGetItemById(_inventory.ItemIdBy(_inventory.SelectedSlotIndex), out var handItem))
                _hand.ChangeItem(handItem);
        }

        private void OnMoved(int indexFrom, int indexTo)
        {
            if (_inventory.Merge(indexFrom, indexTo) == false)
                _inventory.Move(indexFrom, indexTo);

            _inventory.Visualize(_inventoryView);
        }

        private void OnDropped(int indexFrom)
        {
            var id = _inventory.ItemIdBy(indexFrom);
            var itemsCount = _inventory.ItemsCountInSelectedSlot;

            if (_itemsList.TryGetItemById(id, out var item) == false)
                return;

            if (_inventory.TryRemove(indexFrom, itemsCount) == false)
                return;

            _inventory.Visualize(_inventoryView);
        }

        public void Dispose()
        {
            _inventoryView.Moved -= OnMoved;
            _inventoryView.Dropped -= OnDropped;
        }
    }
}
