using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    [CreateAssetMenu(menuName = "Create CollectableItem", fileName = "CollectableItem", order = 56)]
    public class CollectableItem : ScriptableObject, ICollectableItem
    {
        [field: Header("Collectable item information")]
        [field: SerializeField, Range(0, 1)] public float MinProgressFractionRequired { get; private set; }
        [field: SerializeField, Range(0, 1)] public float SizeBoostPercentage { get; private set; }
        [field: SerializeField, Min(0)] public float RespawnDelay { get; private set; }
    }
}
