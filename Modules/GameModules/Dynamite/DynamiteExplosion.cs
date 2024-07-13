using Agava.Audio;
using UnityEngine;

namespace Agava.Playground3D.Dynamite
{
    public class DynamiteExplosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private SoundSource _soundSource;

        private void Awake()
        {
            _effect.Play();
            _soundSource.Play();
        }
    }
}
