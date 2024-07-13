using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Agava.Playground3D.Bots
{
    internal interface IPath
    {
        PathCompleteState CompleteState { get; }
        List<Vector3> VectorPath { get; }
    }
}
