using Agava.DroppedItems;
using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Input;
using Agava.Playground3D.Inventory;
using Agava.Playground3D.Items;

namespace Agava.Playground3D.InventoryRouterFactories
{
    public class SandboxInventoryRouterFactory : InventoryRouterFactory
    {
        public SandboxInventoryRouterFactory(IInput input, Hand hand, ItemsList itemsList) : base(input, hand, itemsList) { }

        public override IInventoryRouter Create(IInventory inventory, IInventoryView inventoryView, TakenItemsList takenItemsList)
        {
            return new SandboxInventoryRouter(input, inventory, inventoryView, takenItemsList, itemsList, hand);
        }
    }
}
