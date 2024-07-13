using Agava.Playground3D.NewYearEvent;
using BehaviorDesigner.Runtime;
using System;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedCollectingCharacter : SharedVariable<CollectingCharacter>
    {
        public static implicit operator SharedCollectingCharacter(CollectingCharacter value) => new SharedCollectingCharacter { Value = value };
    }
}
