using BehaviorDesigner.Runtime;
using Agava.Playground3D.NewYearEvent;

namespace Agava.Playground3D.Bots
{
    internal class SharedCollectableItemsSpawnPoint : SharedVariable<CollectableItemSpawnPoint>
    {
        public static implicit operator SharedCollectableItemsSpawnPoint(CollectableItemSpawnPoint value) => new SharedCollectableItemsSpawnPoint { Value = value };
    }
}
