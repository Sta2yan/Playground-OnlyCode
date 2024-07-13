using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class PredefinedPath : IPath
    {
        private const float YOffset = 1f;
        private const float SecondAxeOffset = 0.5f;

        private readonly Vector3[] _vectorPath;

        public List<Vector3> VectorPath => new List<Vector3>(_vectorPath);
        public PathCompleteState CompleteState => PathCompleteState.Complete;

        public PredefinedPath(Vector3 startWayPointPosition, int endPointOffset, bool zOffset = false)
        {
            float startY = startWayPointPosition.y;
            float startX = startWayPointPosition.x;
            float startZ = startWayPointPosition.z;

            _vectorPath = new Vector3[endPointOffset + 1];

            float currentX = zOffset ? startX + SecondAxeOffset : startX;
            float currentZ = zOffset ? startZ : startZ + SecondAxeOffset;

            for (int i = 0; i < endPointOffset + 1; i++)
            {
                if (zOffset)
                {
                    currentZ = startZ + i;
                }
                else
                {
                    currentX = startX + i;
                }

                _vectorPath[i] = new Vector3(currentX, startY + YOffset, currentZ);
            }
        }

        public PredefinedPath(List<Vector3> vectorPath)
        {
            _vectorPath = vectorPath.ToArray();
        }

        public PredefinedPath ReversePath()
        {
            List<Vector3> wayPoints = VectorPath;
            wayPoints.Reverse();
            return new PredefinedPath(wayPoints);
        }

        public PredefinedPath MergePath(PredefinedPath otherPath)
        {
            List<Vector3> wayPoints = VectorPath;
            wayPoints.AddRange(otherPath.VectorPath);
            return new PredefinedPath(wayPoints);
        }
    }
}
