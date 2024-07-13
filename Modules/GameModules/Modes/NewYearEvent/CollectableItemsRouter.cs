using Agava.AdditionalPredefinedMethods;

namespace Agava.Playground3D.NewYearEvent
{
    public class CollectableItemsRouter : IGameLoop
    {
        private readonly CollectableItemSpawnPoint[] _spawnPoints;
        private readonly DelayedRespawn _delayedRespawn;
        private readonly CollectingCharacter _targetCharacter;
        private readonly CollectedItemsView _view;

        private int _collectedItems = 0;

        public CollectableItemsRouter(CollectableItemSpawnPoint[] spawnPoints, DelayedRespawn delayedRespawn, CollectingCharacter targetCharacter, CollectedItemsView view)
        {
            _spawnPoints = spawnPoints;
            _delayedRespawn = delayedRespawn;
            _targetCharacter = targetCharacter;
            _view = view;
        }

        public void Update(float _)
        {
            foreach (CollectableItemSpawnPoint item in _spawnPoints)
            {
                if (item.ItemCollected)
                {
                    _delayedRespawn.Respawn(item);

                    if (item.Collector == _targetCharacter)
                        _view.UpdateValue(++_collectedItems);
                }
            }
        }
    }
}
