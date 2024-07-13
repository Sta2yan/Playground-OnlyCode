using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public interface ICollectableItem
    {
        public float SizeBoostPercentage { get; }
        public float RespawnDelay { get; }
    }
}
