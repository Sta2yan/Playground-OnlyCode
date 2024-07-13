using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Combat
{
    public class DelayedRespawn
    {
        private readonly Dictionary<ICombatCharacter, Vector3> _respawningCharacters;
        private readonly MonoBehaviour _root;
        private readonly float _delay;

        public DelayedRespawn(MonoBehaviour root, float delaySeconds)
        {
            _root = root;
            _delay = delaySeconds;
            _respawningCharacters = new Dictionary<ICombatCharacter, Vector3>();
        }

        public void Respawn(ICombatCharacter character, Vector3 spawnPosition)
        {
            if (_respawningCharacters.ContainsKey(character))
                return;

            _respawningCharacters.Add(character, spawnPosition);
            _root.StartCoroutine(RespawnWithDelay(character, _delay));
        }

        private IEnumerator RespawnWithDelay(ICombatCharacter character, float delay)
        {
            yield return new WaitForSeconds(delay);

            var respawnPosition = _respawningCharacters[character];
            character.Move(respawnPosition);
            character.Revive();
            
            _respawningCharacters.Remove(character);
        }
    }
}
