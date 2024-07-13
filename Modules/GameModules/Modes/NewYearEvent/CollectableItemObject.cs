using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class CollectableItemObject : MonoBehaviour
    {
        [field: SerializeField] CollectableItem _item;

        public float SizeBoostPercentage => _item.SizeBoostPercentage;
        public float RespawnDelay => _item.RespawnDelay;
        public float MinProgressFractionRequired => _item.MinProgressFractionRequired;
    }
}
