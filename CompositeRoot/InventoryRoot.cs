using Agava.AdditionalPredefinedMethods;
using Agava.DroppedItems;
using Agava.Inventory;
using Agava.Playground3D.Input;
using Agava.Playground3D.Inventory;
using System.Collections.Generic;
using UnityEngine;
using Agava.Playground3D.InventoryRouterFactories;
using System;

namespace Agava.Playground3D.CompositeRoot
{
    public class InventoryRoot : CompositeRoot
    {
        [SerializeField] private List<SlotView> _fastAccessSlotViews;
        [SerializeField] private TakenItemsList _takenItemsList;

        private IInventory _inventory;
        private IInventoryView _inventoryView;
        private IInventoryRouter _inventoryRouter;
        private IGameLoop _gameLoop;

        public void Initialize(IInventory inventory, IInventoryView inventoryView, InventoryRouterFactory factory)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
            _inventoryRouter = factory.Create(_inventory, _inventoryView, _takenItemsList);
        }

        public override void Compose()
        {
            foreach (var slotView in _fastAccessSlotViews)
                slotView.gameObject.AddComponent<InventorySlotInput>().Init(slotView, _inventory, _inventoryView);

            _gameLoop = new GameLoopGroup(_inventoryRouter as IGameLoop);
        }

        public void Update()
        {
            if (_gameLoop != null)
                _gameLoop.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            (_inventoryRouter as IDisposable).Dispose();
        }
    }
}
