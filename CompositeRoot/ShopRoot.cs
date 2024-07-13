using Agava.Playground3D.Shop;
using Agava.AdditionalPredefinedMethods;
using Agava.Playground3D.ShopRouterFactories;
using UnityEngine;
using Agava.Inventory;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.CompositeRoot
{
    public class ShopRoot : CompositeRoot
    {
        [SerializeField] private Agava.Shop.Shop _shop;

        private LevelGate _levelGate;

        private IInventory _inventory;
        private IInventoryView _inventoryView;
        private IShopRouter _shopRouter;
        private IGameLoop _gameLoop;

        public void Initialize(IInventory inventory, IInventoryView inventoryView, ShopRouterFactory shopRouterFactory, LevelGate levelGate = null)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
            _shopRouter = shopRouterFactory.Create(_shop, _inventory, _inventoryView);
            _levelGate = levelGate;
        }

        public override void Compose()
        {
            _shop.Initialize(new ItemsWalletAdapter(_inventory, _inventoryView), _levelGate);

            _gameLoop = new GameLoopGroup(_shopRouter as IGameLoop);
        }

        public void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }
    }
}
