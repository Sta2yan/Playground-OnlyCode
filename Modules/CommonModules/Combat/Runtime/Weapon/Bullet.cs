using System;
using UnityEngine;

namespace Agava.Combat
{
    internal class Bullet : MonoBehaviour
    {
        private const float DelayToEnableKinematic = .1f;

        [SerializeField] private int _damage;
        [SerializeField] private int _delayToDestroy = 5;
        [SerializeField] private float _hitPush = 3f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem[] _redEffectsOnHit;
        [SerializeField] private ParticleSystem[] _notRedEffectsOnHit;

        private ICombatCharacter _without;
        private bool _canEnableKinematic = false;
        private bool _isRedBlood = true;
        private Action _onKill;
        private Action _onHit;

        public void Push(Vector3 direction, ICombatCharacter without, bool isRedBlood, Action onKill = null, Action onHit = null)
        {
            _without = without;
            _rigidbody.AddForce(direction, ForceMode.VelocityChange);
            Invoke(nameof(EnableKinematic), DelayToEnableKinematic);
            _onKill = onKill;
            _onHit = onHit;
            _isRedBlood = isRedBlood;
            Destroy(gameObject, _delayToDestroy);
        }

        private void EnableKinematic()
        {
            _canEnableKinematic = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_canEnableKinematic)
                _rigidbody.isKinematic = true;

            GameObject targetObject = collision.gameObject;
            BulletTarget target = targetObject.GetComponentInChildren<BulletTarget>();

            if (target != null || (target == null && targetObject.TryGetComponent(out target)))
                target.Hit();

            if (targetObject.TryGetComponent(out ICombatCharacter combatCharacter) == false || combatCharacter == _without)
                return;

            CombatCharacter character = combatCharacter as CombatCharacter;
            ParticleSystem particle;
            Vector3 contact = collision.GetContact(0).point;

            if (_isRedBlood)
                CreateVFX(_redEffectsOnHit);
            else
                CreateVFX(_notRedEffectsOnHit);

            combatCharacter.Hit(_damage, Vector3.up * _hitPush);
            _onHit?.Invoke();

            if (combatCharacter.Alive == false)
                _onKill?.Invoke();

            Destroy(gameObject);

            void CreateVFX(ParticleSystem[] particles)
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particle = Instantiate(particles[i], character.transform);
                    particle.transform.position = new Vector3(particle.transform.position.x + UnityEngine.Random.Range(-0.2f, 0.2f), contact.y, particle.transform.position.z);
                    particle.transform.LookAt(contact);
                }
            }
        }
    }
}
