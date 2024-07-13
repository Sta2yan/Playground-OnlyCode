using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.DroppedItems.Sample
{
    public class ItemInteractions : MonoBehaviour
    {
        [SerializeField] private TakenItemsList _takenItemsList;
        [SerializeField] private DroppedItemFactory _droppedItemFactory;
        [SerializeField] private Mesh _droppableItemMesh;
        [SerializeField] private Button _dropButton;

        private void OnEnable()
        {
            _dropButton.onClick.AddListener(Drop);
        }

        private void OnDisable()
        {
            _dropButton.onClick.RemoveListener(Drop);
        }

        private void Update()
        {
            if (_takenItemsList.HasItems)
                Debug.Log($"Took item with id: {_takenItemsList.Pop()}");
        }

        private void Drop()
        {
            //_droppedItemsProducer.Create(Guid.NewGuid().ToString(), Vector3.forward);
        }
    }
}
