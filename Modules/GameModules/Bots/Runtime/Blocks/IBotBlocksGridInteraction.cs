using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public interface IBotBlocksGridInteraction
    {
        bool TryPlaceBlock(Vector3 position);
        bool TryRemoveBlock(Vector3 position);
    }
}
