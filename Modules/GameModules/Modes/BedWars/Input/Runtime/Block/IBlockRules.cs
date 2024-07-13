using UnityEngine;

namespace Agava.Playground3D.Input
{
    public interface IBlockRules
    {
        float PlaceDistance { get; }
        bool CanPlace(Vector3 positionHit, Vector3 scale);
        bool CanRemove(Vector3 blockPosition);
    }
}
