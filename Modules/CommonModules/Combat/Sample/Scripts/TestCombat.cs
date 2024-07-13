using System;
using UnityEngine;

namespace Agava.Combat.Sample
{
    public class TestCombat : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _hitDamage;
        [SerializeField, Min(0)] private float _pushForce;
        [SerializeField] private Vector3 _pushDirection;
        [SerializeField] private CombatCharacter _combatCharacter;

        private void OnValidate()
        {
            _pushDirection = Vector3.ClampMagnitude(_pushDirection, 1f);
        }

        private void Update()
        {
            if (_combatCharacter == null)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space) == false)
                return;
            
            _combatCharacter.Hit(_hitDamage, _pushDirection * _pushForce);
            
            if (_combatCharacter.Alive == false)
                Destroy(_combatCharacter.gameObject);
        }

        private void OnDrawGizmos()
        {
            if (_combatCharacter == null)
                return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_combatCharacter.transform.position, _combatCharacter.transform.position + _pushDirection);
        }
    }
}
