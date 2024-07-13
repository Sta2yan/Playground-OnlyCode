using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Agava.Combat
{
    public class Sword : MonoBehaviour
    {
        private const float AttackRange = 5;

        private readonly Vector3 hitSize = Vector3.one;
        
        [SerializeField, Min(0)] private int _damage;
        [SerializeField, Min(0)] private float _attackForce;
        [SerializeField, Min(0)] private float _attackDelay;
        [SerializeField] private LayerMask _attackLayers;
        [Header("HitBox")]
        [SerializeField] private Transform _topPoint;
        [SerializeField] private Transform _bottomPoint;

        private float _elapsedTime;
        private bool _canAttack;
        private LayerMask _defaultAttackLayers;
        private int _layer;

        public bool CanAttack => _canAttack;

        private void Update()
        {
            if (_canAttack)
                return;

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime < _attackDelay)
                return;

            _elapsedTime = 0;
            _canAttack = true;
        }

        public int Attack(Vector3 forward, IEnumerable<ICombatCharacter> without = null, ITeam[] friendlyTeams = null, Action onHit = null)
        {
            if (CanAttack == false)
                throw new InvalidOperationException();

            RaycastHit[] hits = Physics.BoxCastAll(transform.position, hitSize / 2.0f, forward, Quaternion.identity, AttackRange, _attackLayers);

            if (hits.Length == 0)
                return 0;

            ICombatCharacter targetCharacter = null;
            Collider targetCollider;
            bool friendly;

            foreach (RaycastHit hit in hits)
            {
                targetCollider = hit.collider;

                if (targetCollider.TryGetComponent(out ICombatCharacter target))
                {
                    friendly = friendlyTeams == null ? false : friendlyTeams.Any(team => team.HasCharacter(target));

                    if ((without.Contains(target) || friendly) == false)
                    {
                        targetCharacter = target;
                        break;
                    }
                }
            }

            int kills = 0;

            if (targetCharacter != null)
            {
                targetCharacter.Hit(_damage, Vector3.up * _attackForce);
                onHit?.Invoke();

                if (targetCharacter.Alive == false)
                    kills += 1;
            }

            _canAttack = false;
            return kills;
        }

        public void ChangeAttackLayer(int attackLayers)
        {

            if(_attackLayers.value != attackLayers && attackLayers !=0)
            {
                _layer = _attackLayers.value;
            }

            if(attackLayers == 0)
            {
                _attackLayers.value = _layer;
                return;
            }

            _attackLayers.value = attackLayers;
        }
    }
}
