using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System;
using System.Linq;

namespace Agava.Playground3D.Bots
{
    public class ChooseEnemyTeamIsland : BehaviorDesigner.Runtime.Tasks.Action
    {
        public SharedGameObjectList EnemyTeamIsland;
        public SharedGameObject TargetIsland;

        public override TaskStatus OnUpdate()
        {
            List<GameObject> islands = EnemyTeamIsland.Value;

            if (islands == null)
                return TaskStatus.Failure;

            Func<GameObject, float> islandDistance = (GameObject island) => Vector3.Distance(transform.position, island.transform.position);
            GameObject targetIsland = islands.Find(island => islandDistance(island) == islands.Min(island => islandDistance(island)));
            TargetIsland.Value = targetIsland;

            return targetIsland == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
