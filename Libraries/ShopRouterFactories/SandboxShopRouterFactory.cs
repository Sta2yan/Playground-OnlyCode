using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Items;
using Agava.Playground3D.Shop;

namespace Agava.Playground3D.ShopRouterFactories
{
    public class SandboxShopRouterFactory : ShopRouterFactory
    {
        public SandboxShopRouterFactory(IInput input, ItemsList itemsList) : base(input, itemsList) { }

        public override IShopRouter Create(Agava.Shop.Shop shop, IInventory inventory, IInventoryView inventoryView)
        {
            return new SandboxShopRouter(input, shop, inventory, inventoryView, itemsList);
        }
    }
}
