using System;
using Agava.AdditionalPredefinedMethods;
using Agava.DroppedItems;
using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Items;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.Input
{
    internal class InventoryInputRouter : IGameLoop, IDisposable
    {
        private const int CountDropItems = 1;

        private readonly IInput _input;
        private readonly InventoryView _inventoryView;
        private readonly DroppedItemCommunication _droppedItemCommunication;
        private readonly Inventory.Inventory _inventory;
        private readonly TakenItemsList _takenItemsList;
        private readonly ItemsList _itemsList;
        private readonly Hand _hand;

        private readonly ExperienceEventsContainer _experienceEventsContainer;
        private readonly IItemExperienceEventRule _pickUpItemEventRule;

        public InventoryInputRouter(IInput input, Inventory.Inventory inventory, InventoryView inventoryView,
            DroppedItemCommunication droppedItemCommunication,
            TakenItemsList takenItemsList, ItemsList itemsList, Hand hand,
            ExperienceEventsContainer experienceEventsContainer,
            IItemExperienceEventRule pickUpItemEventRule)
        {
            _input = input;
            _inventory = inventory;
            _inventoryView = inventoryView;
            _droppedItemCommunication = droppedItemCommunication;
            _takenItemsList = takenItemsList;
            _itemsList = itemsList;
            _hand = hand;

            _inventoryView.Moved += OnMoved;
            _inventoryView.Dropped += OnDropped;

            _experienceEventsContainer = experienceEventsContainer;
            _pickUpItemEventRule = pickUpItemEventRule;
        }

        public void Update(float _)
        {
            if (_input.InventoryItemSelected(out var index))
            {
                _inventory.Select(index);
                _inventory.Visualize(_inventoryView);
            }

            if (_input.DropItem())
                OnDropped(_inventory.SelectedSlotIndex, CountDropItems);

            while (_takenItemsList.HasItems)
            {
                if (_itemsList.TryGetItemById(_takenItemsList.Pop(), out var item) == false)
                    continue;

                if (_pickUpItemEventRule.TryGetExperienceEvent(item, out ExperienceEvent experienceEvent))
                    _experienceEventsContainer.TriggerEvent(experienceEvent);

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

            if (_itemsList.TryGetItemById(id, out var item) && item.CanDrop == false)
                return;

            if (_inventory.TryRemove(indexFrom, itemsCount) == false)
                return;

            _inventory.Visualize(_inventoryView);

            for (var i = 0; i < itemsCount; i++)
                RemoveItemBy(id);
        }

        private void OnDropped(int indexFrom, int count)
        {
            var id = _inventory.ItemIdBy(indexFrom);

            if (_itemsList.TryGetItemById(id, out var item) && item.CanDrop == false)
                return;

            if (_inventory.TryRemove(indexFrom, count) == false)
                return;

            _inventory.Visualize(_inventoryView);
            RemoveItemBy(id);
        }

        private void RemoveItemBy(string id)
        {
            if (_itemsList.TryGetItemById(id, out var item) == false)
                throw new InvalidOperationException();

            _droppedItemCommunication.DropFromUser(item);
        }

        public void Dispose()
        {
            _inventoryView.Moved -= OnMoved;
            _inventoryView.Dropped -= OnDropped;
        }
    }
}
