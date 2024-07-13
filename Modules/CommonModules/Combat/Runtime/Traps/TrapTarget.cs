using UnityEngine;

namespace Agava.Combat
{
    public class TrapTarget : MonoBehaviour
    {
        [SerializeField] private CombatCharacter _character;
        [SerializeField, Min(0)] private int _pushForce;
        
        public void Execute(int damage)
        {
            _character.Hit(damage, _pushForce * Vector3.up);
        }
    }
}
