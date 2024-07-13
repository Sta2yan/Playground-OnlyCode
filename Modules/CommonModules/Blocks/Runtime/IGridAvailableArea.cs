using UnityEngine;

namespace Agava.Blocks
{
    public interface IGridAvailableArea
    {
        bool Has(Vector3Int position);
    }
}