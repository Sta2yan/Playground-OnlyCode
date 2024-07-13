using System;
using BehaviorDesigner.Runtime;
using Agava.Combat;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedCombatCharacter : SharedVariable<CombatCharacter>
    {
        public static implicit operator SharedCombatCharacter(CombatCharacter value) => new SharedCombatCharacter { Value = value };
    }
}
