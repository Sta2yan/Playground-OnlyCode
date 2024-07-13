using BehaviorDesigner.Runtime;
using System;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedResourceGenerator : SharedVariable<ResourceGenerator.ResourceGenerator>
    {
        public static implicit operator SharedResourceGenerator(ResourceGenerator.ResourceGenerator value) { return new SharedResourceGenerator { Value = value }; }
    }
}
