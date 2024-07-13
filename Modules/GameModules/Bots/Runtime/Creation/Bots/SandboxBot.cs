using Agava.Combat;
using UnityEngine;
using Agava.Playground3D.Items;

namespace Agava.Playground3D.Bots
{
    internal class SandboxBot : MonoBehaviour, IBot<SandboxBotComposer>
    {
        [SerializeField] private SandboxBotComposer _composer;
        [SerializeField] private CombatCharacter _combatCharacter;
        [SerializeField] private BotSpawnItem _spawnItem;

        public SandboxBotComposer BotComposer => _composer;
        public CombatCharacter CombatCharacter => _combatCharacter;
        public BotSpawnItem SpawnItem => _spawnItem;
    }
}
