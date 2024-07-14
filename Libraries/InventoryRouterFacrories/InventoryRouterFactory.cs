using Agava.DroppedItems;
using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Input;
using Agava.Playground3D.Inventory;
using Agava.Playground3D.Items;

namespace Agava.Playground3D.InventoryRouterFactories
{
    public abstract class InventoryRouterFactory
    {
        protected readonly IInput input;
        protected readonly Hand hand;
        protected readonly ItemsList itemsList;

        public InventoryRouterFactory(IInput input, Hand hand, ItemsList itemsList)
        {
            this.input = input;
            this.hand = hand;
            this.itemsList = itemsList;
        }

        public abstract IInventoryRouter Create(IInventory inventory, IInventoryView inventoryView, TakenItemsList takenItemsList);
    }
}
