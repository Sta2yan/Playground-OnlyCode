using UnityEngine;
using Agava.Inventory;

namespace Agava.Shop.Sample
{
    public class ShopInventoryTest : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private InventoryTest _inventoryTest;

        private InventoryItem _currentInventoryTest;

        public void CreateNewItem(string id, Sprite icon)
        {
            _currentInventoryTest = new InventoryItem(id, 3, icon);
        }

        public void Add(int count)
        {
            for (int i = 0; i < count; i++)
                _inventoryTest.Inventory.Add(_currentInventoryTest);

            _inventoryTest.Inventory.Visualize(_inventoryView);
        }
    }
}
