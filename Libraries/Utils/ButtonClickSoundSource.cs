using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.Audio;

namespace Agava.Utils
{
    public class ButtonClickSoundSource : MonoBehaviour
    {
        [SerializeField] private SoundSource _soundSource;

        private static ButtonClickSoundSource instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void Play()
        {
            if (instance != null)
                instance._soundSource.Play();
        }
    }
}
