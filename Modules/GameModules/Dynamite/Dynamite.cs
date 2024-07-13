using System.Collections;
using UnityEngine;
using Agava.Blocks;

namespace Agava.Playground3D.Dynamite
{
    public class Dynamite : MonoBehaviour, IBlockDamageSource
    {
        [SerializeField] private DynamiteExplosion _explosion;
        [SerializeField] private ParticleSystem _blocksBreakingEffect;
        [Header("Parameters")]
        [SerializeField, Min(0)] private int _damage;
        [SerializeField, Min(0)] private float _activationDelay;
        [SerializeField, Min(0)] private float _explosionDelay;
        [SerializeField, Min(0)] private float _radius;
        [SerializeField, Min(0)] private float _pushForce;

        private float _currentTime;

        public Collider[] Colliders { get; private set; }
        public int Damage => _damage;
        public float PushForce => _pushForce;
        public float ActivationDelay => _activationDelay;
        public float ExplosionDelay => _explosionDelay;
        public bool Active { get; private set; }

        public ParticleSystem Effect => _blocksBreakingEffect;

        private void Update()
        {
            if (Active)
                _currentTime += Time.deltaTime;
        }

        public void Activate(float delay)
        {
            if (Active)
                return;

            Active = true;

            if (delay == 0.0f)
            {
                Execute();
            }
            else
            {
                StartCoroutine(TimeToDestroy(delay));
            }
        }
        
        private void Execute()
        {
            Colliders = Physics.OverlapSphere(transform.position, _radius);
            FindObjectOfType<DynamiteContainer>().AddDynamite(this);
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }

        private IEnumerator TimeToDestroy(float delay)
        {
            yield return new WaitUntil(() => _currentTime >= delay);

            Execute();
        }
    }
}
