using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class PredefinedPathObject : MonoBehaviour
    {
        [SerializeField] private PathPart[] _pathParts;
        [SerializeField] private GameObject _point1;
        [SerializeField] private GameObject _point2;

        public PredefinedPath Path { get; private set; }
        public PredefinedPath ReversedPath => Path.ReversePath();

        public GameObject Point1 => _point1;
        public GameObject Point2 => _point2;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            Path = new PredefinedPath(new List<Vector3>());
            PredefinedPath path;

            foreach (PathPart pathPart in _pathParts)
            {
                path = new PredefinedPath(pathPart.StartWayPoint.position, pathPart.EndWayPointOffset, pathPart.zOffset);
                Path = Path.MergePath(path);
            }
        }

        public bool IsPath(GameObject point1, GameObject point2, out bool reverse) 
        {
            bool normal = Point1 == point1 && Point2 == point2;
            reverse = Point1 == point2 && Point2 == point1;

            return normal || reverse;
        }

        private void OnValidate()
        {
            Initialize();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            for (int i = 0; i < Path.VectorPath.Count - 1; i += 1)
            {
                Gizmos.DrawLine(Path.VectorPath[i], Path.VectorPath[i + 1]);
            }
        }
    }

    [Serializable]
    struct PathPart
    {
        [SerializeField] private Transform _startWayPoint;
        [SerializeField, Min(0)] private int _endWayPointOffset;
        [SerializeField] private bool _zOffset;

        public Transform StartWayPoint => _startWayPoint;
        public int EndWayPointOffset => _endWayPointOffset;
        public bool zOffset => _zOffset;
    }

}
