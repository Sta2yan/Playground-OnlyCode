using Agava.Input;
using Agava.Inventory;
using Agava.Playground3D.Items;
using Agava.Playground3D.Shop;

namespace Agava.Playground3D.ShopRouterFactories
{
    public abstract class ShopRouterFactory
    {
        protected readonly IInput input;
        protected readonly ItemsList itemsList;

        public ShopRouterFactory(IInput input, ItemsList itemsList)
        {
            this.input = input;
            this.itemsList = itemsList;
        }

        public abstract IShopRouter Create(Agava.Shop.Shop shop, IInventory inventory, IInventoryView inventoryView);
    }
}
