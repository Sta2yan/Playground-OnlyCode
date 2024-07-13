using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class CollectableItemSpawnPoint : MonoBehaviour
    {
        [field: SerializeField] CollectableItemObject _item;

        public float SizeBoostPercentage => _item.SizeBoostPercentage;
        public float RespawnDelay => _item.RespawnDelay;

        public bool ItemCollected { get; private set; } = false;
        public CollectingCharacter Collector { get; private set; } = null;

        private void OnTriggerEnter(Collider other)
        {
            if (ItemCollected)
                return;

            if (other.TryGetComponent(out CollectingCharacter collectingCharacter))
            {
                if (CanCollect(collectingCharacter))
                {
                    ItemCollected = true;
                    Collector = collectingCharacter;
                    _item.gameObject.SetActive(false);
                    collectingCharacter.Collect(this);
                }
            }
        }

        public bool CanCollect(CollectingCharacter collectingCharacter) => collectingCharacter.ProgressFraction >= _item.MinProgressFractionRequired;

        public void RespawnItem()
        {
            ItemCollected = false;
            Collector = null;
            _item.gameObject.SetActive(true);
        }
    }
}
