using UnityEngine;

namespace Agava.Shop.Sample
{
    public class ShopInterlayer : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private ShopInventoryTest _shopInventoryTest;

        private void Awake()
        {
            _shop.Initialize(new TestItemsWallet());
        }

        private void Update()
        {
            if (_shop.HasPurchasedProducts == false)
                return;

            var purchasedItem = _shop.PopPurchasedProduct();
            _shopInventoryTest.CreateNewItem(purchasedItem.Id, null);
            _shopInventoryTest.Add(purchasedItem.Count);
        }
    }
}