using UnityEngine;

namespace Agava.Utils
{
    public class DestroyParticleSystem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void Update()
        {
            if (_particleSystem.isPlaying == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
