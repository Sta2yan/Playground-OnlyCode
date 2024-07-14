using UnityEngine;

namespace Agava.Audio
{
    public static class EnableAudio
    {
        public static bool Enabled { get; private set; } = true;

        public static void Enable()
        {
            AudioListener.volume = 1;
            Enabled = true;
        }

        public static void Disable()
        {
            AudioListener.volume = 0;
            Enabled = false;
        }
    }
}
