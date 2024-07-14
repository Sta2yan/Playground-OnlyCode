using UnityEngine;

namespace Agava.Audio
{
    public class SoundSource : MonoBehaviour, ISoundSource
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void Play()
        {
            _audioSource.Play();
        }
    }
}
