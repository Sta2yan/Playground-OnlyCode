using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Agava.Playground3D.PathFinding
{
    public class PathFindingUpdate
    {
        private readonly Vector3 BlockSize = Vector3.one;

        private Stack<Vector3> _updatingPositions = new();

        public bool UpdateRequired => _updatingPositions.Count > 0;

        public void UpdateGraph()
        {
            if (_updatingPositions.TryPop(out Vector3 position))
            {
                Bounds bounds = new Bounds(position, BlockSize);

                while (_updatingPositions.TryPop(out position))
                    bounds.Encapsulate(position);

                UpdateGraphInBounds(bounds);
            }
        }

        public void RequestUpdateAtPosition(Vector3 position)
        {
            _updatingPositions.Push(position);
        }

        private void UpdateGraphInBounds(Bounds bounds)
        {
            if (AstarPath.active)
            {
                GraphUpdateObject graphUpdate = new GraphUpdateObject(bounds);
                graphUpdate.updatePhysics = true;

                AstarPath.active.UpdateGraphs(graphUpdate);
            }
        }
    }
}
