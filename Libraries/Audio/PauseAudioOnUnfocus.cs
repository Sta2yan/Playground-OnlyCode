using UnityEngine;

namespace Agava.Audio
{
    public class PauseAudioOnUnfocus : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                PauseAudio.Unpause();
            }
            else
            {
                PauseAudio.Pause();
            }
        }
    }
}