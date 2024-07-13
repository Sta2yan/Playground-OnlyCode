using UnityEngine;
using Pathfinding;

namespace Agava.Playground3D.Bots
{
    internal class PathIteration
    {
        private readonly IPath _path;

        public PathIteration(IPath path)
        {
            _path = path;
            CurrentWayPoint = 0;
        }

        public int CurrentWayPoint { get; private set; }
        public bool ReachedEnd => CurrentWayPoint >= WayPointCount - 1;
        public int WayPointCount => _path.VectorPath.Count;
        public Vector3 WayPointPosition => _path.VectorPath[CurrentWayPoint];
        public bool PathNotCalculated => _path.CompleteState == PathCompleteState.NotCalculated;
        public bool PathCompleted => PathNotCalculated ? false : _path.CompleteState == PathCompleteState.Complete;

        public void NextWayPoint() => CurrentWayPoint++;
    }
}
