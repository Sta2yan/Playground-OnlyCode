using Agava.Combat;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class NewYearEventBot : MonoBehaviour, IBot<NewYearEventBotComposer>
    {
        [field: SerializeField] public NewYearEventBotComposer BotComposer { get; private set; }
        [field: SerializeField] public CombatCharacter CombatCharacter { get; private set; }
    }
}
