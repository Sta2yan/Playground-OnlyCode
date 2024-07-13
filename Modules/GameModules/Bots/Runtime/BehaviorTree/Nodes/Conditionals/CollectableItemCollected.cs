using BehaviorDesigner.Runtime.Tasks;
using Agava.Playground3D.NewYearEvent;

namespace Agava.Playground3D.Bots
{
    internal class CollectableItemCollected : Conditional
    {
        public SharedCollectableItemsSpawnPoint TargetSpawnPoint;

        public override TaskStatus OnUpdate()
        {
            CollectableItemSpawnPoint targetSpawnPoint = TargetSpawnPoint.Value;

            return targetSpawnPoint.ItemCollected ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
