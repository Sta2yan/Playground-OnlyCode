using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class DelayedRespawn
    {
        private readonly List<CollectableItemSpawnPoint> _respawningItems;
        private readonly MonoBehaviour _root;
        private readonly CollectableItemsBotContainer _container;

        public DelayedRespawn(MonoBehaviour root, CollectableItemsBotContainer container)
        {
            _root = root;
            _container = container;
            _respawningItems = new();
        }

        public void Respawn(CollectableItemSpawnPoint spawnPoint)
        {
            if (_respawningItems.Contains(spawnPoint))
                return;

            _respawningItems.Add(spawnPoint);
            _root.StartCoroutine(RespawnWithDelay(spawnPoint));
        }

        private IEnumerator RespawnWithDelay(CollectableItemSpawnPoint spawnPoint)
        {
            yield return new WaitForSeconds(spawnPoint.RespawnDelay);

            _container.ResetSpawnPoint(spawnPoint);
            spawnPoint.RespawnItem();
            _respawningItems.Remove(spawnPoint);
        }
    }
}
