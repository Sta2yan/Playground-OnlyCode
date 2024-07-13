using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.AdditionalPredefinedMethods;

namespace Agava.Playground3D.PathFinding
{
    public class PathFindingRouter : IGameLoop
    {
        private readonly float _updateDelay;
        private readonly PathFindingUpdate _pathFindingUpdate;

        private float _lastUpdateTime;

        public PathFindingRouter(PathFindingUpdate pathFindingUpdate, float updateDelay)
        {
            _pathFindingUpdate = pathFindingUpdate;
            _updateDelay = updateDelay;
            _lastUpdateTime = 0.0f;
        }

        public void Update(float _)
        {
            float time = Time.time;

            if (_lastUpdateTime + _updateDelay <= time)
            {
                _lastUpdateTime = time;

                //if (_pathFindingUpdate.UpdateRequired)
                //    _pathFindingUpdate.UpdateGraph();
            }
        }

    }
}
