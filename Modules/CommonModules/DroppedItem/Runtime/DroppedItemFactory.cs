using UnityEngine;

namespace Agava.DroppedItems
{
    public class DroppedItemFactory : MonoBehaviour
    {
        [SerializeField] private float _itemPushForce = 10f;
        [SerializeField] private DroppedItem _droppedItemTemplate;

        private Transform _dropRoot;

        public DroppedItem Create(string itemId, Mesh itemMesh, Material itemMaterial, Vector3 pushDirection)
        {
            if (_dropRoot == null)
                _dropRoot = new GameObject("DropRoot").transform;
            
            var droppedItem = Instantiate(_droppedItemTemplate, transform.position, Quaternion.identity, _dropRoot);
            droppedItem.Initialize(itemId, itemMesh, itemMaterial);
            droppedItem.Push(pushDirection, _itemPushForce);

            return droppedItem;
        }
    }
}
    