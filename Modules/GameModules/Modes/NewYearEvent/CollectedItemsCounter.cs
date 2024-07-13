using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class CollectedItemsCounter
    {
        public int Count { get; private set; } = 0;

        public void IncreaseValue()
        {
            Count++;
        }
    }
}
