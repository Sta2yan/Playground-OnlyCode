using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Inventory.Sample
{
    public class InventiryInputHandler : MonoBehaviour, IPointerClickHandler
    {
        private const int _bottomMaxIndex = 8;

        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private InventoryTest _inventoryTest;

        private void OnEnable()
        {
            _inventoryView.Moved += OnMoved;
            _inventoryView.Dropped += OnDroped;
        }

        private void OnDisable()
        {
            _inventoryView.Moved -= OnMoved;
            _inventoryView.Dropped -= OnDroped;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SelectSlot(0);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                SelectSlot(1);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                SelectSlot(2);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                SelectSlot(3);

            if (Input.GetKeyDown(KeyCode.Alpha5))
                SelectSlot(4);

            if (Input.GetKeyDown(KeyCode.Alpha6))
                SelectSlot(5);

            if (Input.GetKeyDown(KeyCode.Alpha7))
                SelectSlot(6);

            if (Input.GetKeyDown(KeyCode.Alpha8))
                SelectSlot(7);

            if (Input.GetKeyDown(KeyCode.Alpha9))
                SelectSlot(8);
        }

        private void SelectSlot(int index)
        {
            _inventoryTest.Inventory.Select(index);
            _inventoryTest.Inventory.Visualize(_inventoryView);
        }

        private void OnMoved(int indexFrom, int indexTo)
        {
            if (_inventoryTest.Inventory.Merge(indexFrom, indexTo) == false)
                _inventoryTest.Inventory.Move(indexFrom, indexTo);

            _inventoryTest.Inventory.Visualize(_inventoryView);

        }
        private void OnDroped(int indexFrom)
        {
            _inventoryTest.Inventory.TryRemove(indexFrom, 1);
            _inventoryTest.Inventory.Visualize(_inventoryView);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out SlotView slotView))
            {
                if(slotView.Index <= _bottomMaxIndex)
                {
                    SelectSlot(slotView.Index);
                }
            }
                
        }
    }
}
