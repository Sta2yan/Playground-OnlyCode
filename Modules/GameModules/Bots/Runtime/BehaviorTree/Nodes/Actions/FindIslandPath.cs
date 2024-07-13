using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class FindIslandPath : Action
    {
        public SharedGameObject CurrentIsland;
        public SharedGameObject TargetIsland;
        public SharedPredefinedPathObjectList PredefinedPathObjectList;
        public SharedPredefinedPath PredefinedPath;
        public SharedVector3 TargetPosition;

        public override TaskStatus OnUpdate()
        {
            GameObject currentIsland = CurrentIsland.Value;
            GameObject targetIsland = TargetIsland.Value;
            List<PredefinedPathObject> pathObjects = PredefinedPathObjectList.Value;

            if (pathObjects == null)
                return TaskStatus.Failure;

            bool pathExists = false;

            PredefinedPath path;

            foreach (PredefinedPathObject pathObject in pathObjects)
            {
                pathExists = pathObject.IsPath(currentIsland, targetIsland, out bool reverse);

                if (pathExists)
                {
                    path = reverse ? pathObject.ReversedPath : pathObject.Path;
                    PredefinedPath.Value = path;
                    TargetPosition.Value = path.VectorPath[0];
                    break;
                }
            }

            return pathExists ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
