using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Combat
{
    public class RenewableCombatPool : MonoBehaviour
    {
        private readonly List<ICombatCharacter> _aliveCharacters = new();
        private readonly Queue<ICombatCharacter> _deadCharacters = new();
        
        [SerializeField] private List<CombatCharacter> _combatCharacters;
        [SerializeField] private float _delayToRespawn;

        private float _currentTime;
        
        private void Start() => Setup();

        private void Update()
        {
            RestoreTimeCount();
            PeekDeadCharacters();
        }
        
        private void Setup()
        {
            foreach (var combat in _combatCharacters)
            {
                if (combat.Alive)
                    _aliveCharacters.Add(combat);
                else
                    _deadCharacters.Enqueue(combat);
            }
        }

        private void RestoreTimeCount()
        {
            if (_deadCharacters.Count > 0)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _delayToRespawn)
                {
                    ResetDeadCharacter(_deadCharacters.Dequeue());
                    _currentTime = 0;
                }
            }
        }
        
        private void PeekDeadCharacters()
        {
            for (int i = 0; i < _aliveCharacters.Count; i++)
            {
                if (_aliveCharacters[i].Alive == false)
                {
                    AddDeadCharacter(_aliveCharacters[i]);
                }
            }
        }
        
        private void AddDeadCharacter(ICombatCharacter character)
        {
            _aliveCharacters.Remove(character);
            _deadCharacters.Enqueue(character);
        }

        private void ResetDeadCharacter(ICombatCharacter character)
        {
            if (character.Alive)
                throw new InvalidOperationException("Character is Alive!");
            
            character.Revive();
            _aliveCharacters.Add(character);
        }
    }
}
