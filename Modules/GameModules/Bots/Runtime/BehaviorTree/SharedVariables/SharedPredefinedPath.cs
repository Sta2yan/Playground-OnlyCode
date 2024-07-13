using System;
using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedPredefinedPath : SharedVariable<PredefinedPath>
    {
        public static implicit operator SharedPredefinedPath(PredefinedPath value) { return new SharedPredefinedPath { Value = value }; }
    }
}
