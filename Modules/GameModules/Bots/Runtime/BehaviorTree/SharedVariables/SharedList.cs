using System;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedList<T> : SharedVariable<List<T>>
    {
        public static implicit operator SharedList<T>(List<T> value) => new SharedList<T> { Value = value };
    }
}