using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class PathWrapper : IPath
    {
        private readonly Path _path;

        public PathWrapper(Path path)
        {
            _path = path;
        }

        public List<Vector3> VectorPath => _path.vectorPath;
        public PathCompleteState CompleteState => _path.CompleteState;
    }
}
