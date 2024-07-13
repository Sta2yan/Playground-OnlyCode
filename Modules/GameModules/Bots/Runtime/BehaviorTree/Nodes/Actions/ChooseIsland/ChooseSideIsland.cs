using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Linq;
using System;

namespace Agava.Playground3D.Bots
{
    internal class ChooseSideIsland : BehaviorDesigner.Runtime.Tasks.Action
    {
        public SharedGameObjectList SideIslandList;
        public SharedGameObject TargetIsland;

        public override TaskStatus OnUpdate()
        {
            List<GameObject> islands = SideIslandList.Value;

            if (islands == null)
                return TaskStatus.Failure;

            Func<GameObject, float> islandDistance = (GameObject island) => Vector3.Distance(transform.position, island.transform.position);
            GameObject targetIsland = islands.Find(island => islandDistance(island) == islands.Min(island => islandDistance(island)));
            TargetIsland.Value = targetIsland;

            return targetIsland == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
