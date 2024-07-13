using Agava.Combat;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class MainMenuBot : MonoBehaviour, IBot<MainMenuBotComposer>
    {
        [field: SerializeField] public MainMenuBotComposer BotComposer { get; private set; }
        [field: SerializeField] public CombatCharacter CombatCharacter { get; private set; } = null;
    }
}
