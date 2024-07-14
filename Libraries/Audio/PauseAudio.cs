using UnityEngine;

namespace Agava.Audio
{
    public static class PauseAudio
    {
        private static bool doublePaused = false;

        public static bool Paused { get; private set; } = false;

        public static void Pause()
        {
            doublePaused = Paused;

            AudioListener.pause = true;
            Paused = true;
        }

        public static void Unpause()
        {
            if (doublePaused)
            {
                doublePaused = false;
                return;
            }

            AudioListener.pause = false;
            Paused = false;
        }
    }
}