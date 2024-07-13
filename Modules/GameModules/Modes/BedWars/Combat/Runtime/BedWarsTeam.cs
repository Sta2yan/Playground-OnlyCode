using Agava.Combat;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Playground3D.BedWars.Combat
{
    public class BedWarsTeam : MonoBehaviour, IBedWarsTeam
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _bed;
        [SerializeField] private Color _color;

        private List<ICombatCharacter> _characters;
        private DelayedRespawn _delayedRespawn;

        public bool Alive => HasBed || _characters.Any(character => character.Alive);
        public bool HasBed => _bed != null;
        public int AliveCharacters => _characters.Count(character => character.Alive);
        public int Characters => _characters.Count;
        public Color Color => _color;

        private void Update()
        {
            if (_characters == null)
                return;

            if (Alive == false)
                return;

            foreach (ICombatCharacter character in _characters)
            {
                if (character.Alive == false)
                    _delayedRespawn.Respawn(character, _spawnPoint.position);
            }
        }

        public void Initialize(IEnumerable<ICombatCharacter> characters, DelayedRespawn delayedRespawn)
        {
            _characters = new List<ICombatCharacter>(characters);
            _delayedRespawn = delayedRespawn;

            foreach (var character in _characters)
                character.Move(_spawnPoint.position);
        }

        public bool HasCharacter(ICombatCharacter combatCharacter)
        {
            return _characters.Contains(combatCharacter);
        }

        public bool FriendlyTeam(ITeam team)
        {
            return team == this as ITeam;
        }
    }
}
