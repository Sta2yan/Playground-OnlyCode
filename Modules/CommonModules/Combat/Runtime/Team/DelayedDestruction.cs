using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Combat
{
    public class DelayedDestruction
    {
        private readonly List<ICombatCharacter> _destructingCharacters;
        private readonly MonoBehaviour _root;

        public DelayedDestruction(MonoBehaviour root)
        {
            _root = root;
            _destructingCharacters = new();
        }

        public void Destroy(ICombatCharacter character, float delay, Action beforeDestruction = null)
        {
            if (_destructingCharacters.Contains(character))
                return;

            beforeDestruction?.Invoke();
            _destructingCharacters.Add(character);
            _root.StartCoroutine(DestroyWithDelay(character, delay));
        }

        private IEnumerator DestroyWithDelay(ICombatCharacter character, float delay)
        {
            WaitForSeconds wait = new WaitForSeconds(delay);

            yield return wait;

            character.Destroy();
            _destructingCharacters.Remove(character);
        }
    }
}
