using Agava.Playground3D.NewYearEvent;
using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    internal class FindClosestCollectableItemsSpawnPoint : Action
    {
        public SharedCollectableItemsBotContainer Container;
        public SharedCollectableItemsSpawnPoint TargetSpawnPoint;
        public SharedCollectingCharacter CollectingCharacter;

        public override TaskStatus OnUpdate()
        {
            CollectableItemsBotContainer container = Container.Value;

            if (container.TryGetClosestSpawnPoint(CollectingCharacter.Value, out CollectableItemSpawnPoint spawnPoint))
                TargetSpawnPoint.Value = spawnPoint;

            return TaskStatus.Success;
        }
    }
}
