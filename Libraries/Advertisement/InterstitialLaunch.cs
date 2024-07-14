using System.Collections;
using UnityEngine;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using Agava.Audio;

namespace Agava.Advertisement
{
    public class InterstitialLaunch : MonoBehaviour
    {
        private Advertisement _advertisement;

        public void Initialize(Advertisement advertisement)
        {
            _advertisement = advertisement;
        }

        public IEnumerator Execute()
        {
            yield return new WaitUntil(() => _advertisement != null);

#if YANDEX_GAMES
            yield return YandexGamesSdk.Initialize();
#endif

            _advertisement.ShowInterstitialAd();

            yield return null;
        }
    }
}
