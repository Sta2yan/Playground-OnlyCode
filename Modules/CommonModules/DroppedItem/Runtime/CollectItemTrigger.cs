using UnityEngine;

namespace Agava.DroppedItems
{
    internal class CollectItemTrigger : MonoBehaviour
    {
        [SerializeField] private TakenItemsList _takenItemsList;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out DroppedItem droppedItem))
            {
                _takenItemsList.Add(droppedItem.Id);
                Destroy(droppedItem.gameObject);
            }
        }
    }
}
