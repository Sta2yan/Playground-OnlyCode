using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class ChooseRandomPosition : Action
    {
        public SharedTransformList Positions;
        public SharedVector3 TargetPosition;

        public override TaskStatus OnUpdate()
        {
            List<Transform> positions = Positions.Value;

            if (positions == null)
                return TaskStatus.Failure;

            Transform randomPosition = positions[Random.Range(0, positions.Count)];
            TargetPosition.Value = randomPosition.position;

            return TaskStatus.Success;
        }
    }
}
