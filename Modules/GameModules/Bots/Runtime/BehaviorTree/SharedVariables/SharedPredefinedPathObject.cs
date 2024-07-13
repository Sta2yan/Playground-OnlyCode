using System;
using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedPredefinedPathObject : SharedVariable<PredefinedPathObject>
    {
        public static implicit operator SharedPredefinedPathObject(PredefinedPathObject value) { return new SharedPredefinedPathObject { Value = value }; }
    }
}
