using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class CollectableItemsBotContainer
    {
        private readonly CollectableItemSpawnPoint[] _spawnPoints;

        private List<CollectableItemSpawnPoint> _targetedSpawnPoints = new();

        public CollectableItemsBotContainer(CollectableItemSpawnPoint[] spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }

        public bool TryGetClosestSpawnPoint(CollectingCharacter collectingCharacter, out CollectableItemSpawnPoint spawnPoint)
        {
            spawnPoint = null;

            float minDistance = float.MaxValue;
            float distance;

            foreach (CollectableItemSpawnPoint point in _spawnPoints)
            {
                if (_targetedSpawnPoints.Contains(point))
                    continue;

                if (point.CanCollect(collectingCharacter) == false)
                    continue;

                if (point.ItemCollected)
                    continue;

                distance = Vector3.Distance(point.transform.position, collectingCharacter.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    spawnPoint = point;
                }
            }

            if (spawnPoint != null)
            {
                _targetedSpawnPoints.Add(spawnPoint);
                return true;
            }

            return false;
        }

        public void ResetSpawnPoint(CollectableItemSpawnPoint spawnPoint)
        {
            _targetedSpawnPoints.Remove(spawnPoint);
        }
    }
}
