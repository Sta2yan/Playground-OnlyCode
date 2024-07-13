using UnityEngine;
using Pathfinding;
using System;
using System.Linq;

namespace Agava.Playground3D.Bots
{
    internal class PathMovement : MonoBehaviour
    {
        private const float MinPathSeekingDistance = 0.9f;

        [SerializeField] Transform _jumpPoint;
        [SerializeField] private Seeker _seeker;
        [SerializeField, Min(0f)] private float _nextWayPointDistance = 2;
        [SerializeField, Min(0f)] private float _minDistanceDelta = 0.5f;

        private IBotMovementExecution _movementExecution;

        private Vector3 _targetPosition;
        private PathIteration _pathIteration;
        private Action _onPathIterationEnd = null;

        public bool PathIterating => _pathIteration != null;

        public void Initialize(IBotMovementExecution botMovementExecution)
        {
            _movementExecution = botMovementExecution;
        }

        public bool TryExecutePathMovement(Vector3 targetPosition, Action onPathIterationEnd = null)
        {
            if (targetPosition == null)
                return false;

            void PathIterationInitialization()
            {
                if (targetPosition != _targetPosition)
                {
                    if (Vector3.Distance(targetPosition, _targetPosition) <= MinPathSeekingDistance)
                        return;

                    _targetPosition = targetPosition;
                    _seeker.CancelCurrentPathRequest();
                    Path path = _seeker.StartPath(transform.position, targetPosition);
                    PathWrapper pathWrapper = new PathWrapper(path);
                    _pathIteration = new PathIteration(pathWrapper);
                }
            }

            return TryExecutePathMovement(PathIterationInitialization, onPathIterationEnd);
        }

        public bool TryExecutePathMovement(Transform target, Action onPathIterationEnd = null)
        {
            if (target == null)
                return false;

            return TryExecutePathMovement(target.position, onPathIterationEnd);
        }

        public bool TryExecutePathMovement(PredefinedPath path, Action onPathIterationEnd = null)
        {
            if (path == null)
                return false;

            void PathIterationInitialization()
            {
                Vector3 targetPosition = path.VectorPath.Last();

                if (targetPosition != _targetPosition)
                {
                    _targetPosition = targetPosition;
                    _pathIteration = new PathIteration(path);
                }
            }

            return TryExecutePathMovement(PathIterationInitialization, onPathIterationEnd);
        }

        public void StopMovement()
        {
            ResetIteration();
        }

        private bool TryExecutePathMovement(Action pathIterationInitialization, Action onPathIterationEnd = null)
        {
            if (_pathIteration != null)
            {
                if (_pathIteration.ReachedEnd)
                    return true;
            }

            pathIterationInitialization();
            _onPathIterationEnd = onPathIterationEnd;

            if (_pathIteration != null)
            {
                if (_pathIteration.PathNotCalculated == false)
                    return _pathIteration.PathCompleted;
            }

            return true;
        }

        private void Update()
        {
            if (_pathIteration == null)
                return;

            if (_pathIteration.PathNotCalculated || (_pathIteration.PathCompleted == false))
                return;

            float wayPointDistance;
            Vector3 nextWayPoint;
            Vector3 wayPointPosition;

            while (true)
            {
                try
                {
                    wayPointPosition = _pathIteration.WayPointPosition;
                }
                catch (ArgumentOutOfRangeException)
                {
                    _onPathIterationEnd?.Invoke();
                    ResetIteration();
                    return;
                }

                nextWayPoint = new Vector3(wayPointPosition.x, transform.position.y, wayPointPosition.z);
                wayPointDistance = Vector3.Distance(transform.position, nextWayPoint);

                if (wayPointDistance < _nextWayPointDistance)
                {
                    if (_pathIteration.CurrentWayPoint < _pathIteration.WayPointCount - 1)
                    {
                        _pathIteration.NextWayPoint();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (_pathIteration.ReachedEnd)
            {
                if (wayPointDistance < _minDistanceDelta)
                {
                    _onPathIterationEnd?.Invoke();
                    ResetIteration();
                    return;
                }
            }

            _movementExecution.ExecuteMovement(_pathIteration.WayPointPosition - _jumpPoint.position);
        }

        private void ResetIteration()
        {
            _onPathIterationEnd = null;
            _pathIteration = null;
            _targetPosition = Vector3.zero;
            _movementExecution.Stop();
        }
    }
}
