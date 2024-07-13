using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Inventory
{
    public class InventoryTest: MonoBehaviour
    {
        private const int MainInventoryCapacity = 37;

        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private Sprite _sprite1;
        [SerializeField] private Sprite _sprite2;
        [SerializeField] private Sprite _sprite3;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _removeForIndexButton;

        private Inventory _inventory;

        private InventoryItem _inventoryItem_1;
        private InventoryItem _inventoryItem_2;
        private InventoryItem _inventoryItem_3;

        private List<InventoryItem> _inventoryItems;

        public Inventory Inventory => _inventory;

        private void OnEnable()
        {
            _addButton.onClick.AddListener(Add);
            _removeButton.onClick.AddListener(Remove);
            _removeForIndexButton.onClick.AddListener(RemoveForIndex);
        }

        private void OnDisable()
        {
            _addButton.onClick.RemoveListener(Add);
            _removeButton.onClick.RemoveListener(Remove);
            _removeForIndexButton.onClick.RemoveListener(RemoveForIndex);
        }

        private void Start()
        {
            _inventory = new Inventory(MainInventoryCapacity);
            _inventoryItems = new List<InventoryItem>();

            _inventory.Visualize(_inventoryView);

            _inventoryItem_1 = new InventoryItem("1", 3, _sprite1);
            _inventoryItem_2 = new InventoryItem("2", 3, _sprite2);
            _inventoryItem_3 = new InventoryItem("3", 3, _sprite3);

            _inventoryItems.Add(_inventoryItem_1);
            _inventoryItems.Add(_inventoryItem_2);
            _inventoryItems.Add(_inventoryItem_3);
        }

        public void Add()
        {
            _inventory.Add(_inventoryItems[Random.Range(0, _inventoryItems.Count)]);
            _inventory.Visualize(_inventoryView);
        }

        private void Remove()
        {
            var item = _inventoryItems[Random.Range(0, _inventoryItems.Count)];
            
            if (_inventory.HasItemsWith(item.Id, 1))
                _inventory.Remove(_inventoryItems[Random.Range(0, _inventoryItems.Count)], 1);
            
            _inventory.Visualize(_inventoryView);
        }

        private void RemoveForIndex()
        {
            _inventory.TryRemove(0, 1);
            _inventory.Visualize(_inventoryView);
        }
    }
}