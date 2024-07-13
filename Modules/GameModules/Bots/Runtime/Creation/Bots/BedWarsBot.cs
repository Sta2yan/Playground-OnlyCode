using Agava.Combat;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class BedWarsBot : MonoBehaviour, IBot<BedWarsBotComposer>
    {
        [SerializeField] private BedWarsBotComposer _composer;
        [SerializeField] private CombatCharacter _combatCharacter;

        public BedWarsBotComposer BotComposer => _composer;
        public CombatCharacter CombatCharacter => _combatCharacter;
    }
}
