using BehaviorDesigner.Runtime;
using System;

namespace Agava.Playground3D.Bots
{
    [Serializable]
    internal class SharedBotEssensials : SharedVariable<BotEssensials>
    {
        public static implicit operator SharedBotEssensials(BotEssensials value) => new SharedBotEssensials { Value = value };
    }
}
