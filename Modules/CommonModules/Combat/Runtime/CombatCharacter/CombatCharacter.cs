using Agava.Audio;
using System;
using UnityEngine;
    
namespace Agava.Combat
{
    public class CombatCharacter : MonoBehaviour, ICombatCharacter
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private HitView _hitView;
        [SerializeField] private GameObject _deathSkinTemplate;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _root;
        [SerializeField] private GameObject _modelRoot;
        [SerializeField] private SoundSource _hitSoundSource;

        private Health _health;

        public bool Alive => _health.Alive;
        public Vector3 Forward => _modelRoot.transform.forward;

        private void Awake()
        {
            _health = new Health(_maxHealth);

            if (_healthView)
                _health.Visualize(_healthView);

            if (_deathView)
                _deathView.Initialize(_deathSkinTemplate);
        }

        public void Hit(int damage, Vector3 pushForce)
        {
            if (Alive == false)
                return;

            _health.TakeDamage(damage);
            _hitSoundSource.Play();

            if (_healthView)
                _health.Visualize(_healthView);

            _rigidbody.AddForce(pushForce, ForceMode.VelocityChange);

            if (_health.Alive)
                _hitView.Execute();

            if (Alive)
                return;

            _root.SetActive(false);
            _deathView.Render(_modelRoot.transform.position, _modelRoot.transform.rotation);
        }

        public void Revive()
        {
            if (Alive)
                throw new InvalidOperationException("Character is Alive!");

            _health = new Health(_maxHealth);

            if (_healthView)
                _health.Visualize(_healthView);

            _root.SetActive(true);
            _deathView.Disable();
        }

        public void Move(Vector3 position)
        {
            _rigidbody.velocity = Vector3.zero;
            _root.transform.position = position;
        }

        public void Destroy()
        {
            _deathView.Disable();
            Destroy(gameObject);
        }
    }
}