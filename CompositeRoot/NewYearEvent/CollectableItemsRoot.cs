using UnityEngine;
using Agava.Playground3D.NewYearEvent;
using Agava.AdditionalPredefinedMethods;

namespace Agava.Playground3D.CompositeRoot
{
    public class CollectableItemsRoot : CompositeRoot
    {
        [SerializeField] private CollectingCharacter _targetCharacter;
        [SerializeField] private CollectedItemsView _view;

        private IGameLoop _gameLoop;
        private CollectableItemSpawnPoint[] _collectableItemsSpawnPoints;
        private CollectableItemsBotContainer _container;

        public void Initialize(CollectableItemSpawnPoint[] collectableItemsSpawnPoints, CollectableItemsBotContainer container)
        {
            _collectableItemsSpawnPoints = collectableItemsSpawnPoints;
            _container = container;
        }

        public override void Compose()
        {
            DelayedRespawn delayedRespawn = new DelayedRespawn(this, _container);

            CollectableItemsRouter collectableItemsRouter = new CollectableItemsRouter(_collectableItemsSpawnPoints, delayedRespawn, _targetCharacter, _view);
            _gameLoop = new GameLoopGroup(collectableItemsRouter);
        }

        private void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }
    }
}
