using Agava.Advertisement;
using UnityEngine;

namespace Agava.Playground3D.MainMenu
{
    public class MainMenuInterstitial : MonoBehaviour
    {
        [SerializeField] private InterstitialLaunch _interstitialLaunch;

        private static bool firstLaunch = true;

        private void Awake()
        {
            bool playInterstitial = true;

#if ANDROID_BUILD
            playInterstitial = !firstLaunch;
#endif

            if (playInterstitial)
            {
                StartCoroutine(_interstitialLaunch.Execute());
            }

            firstLaunch = false;
        }

        private void OnApplicationQuit()
        {
            firstLaunch = true;
        }
    }
}
