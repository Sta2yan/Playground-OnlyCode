using System;

namespace Agava.Advertisement
{
    public class YandexGamesReward : IAdvertisement
    {
        public void ShowInterstitialAd(Action onOpenCallback = null, Action<bool> onCloseCallback = null, Action<string> onErrorCallback = null)
        {
#if YANDEX_GAMES
            YandexGames.InterstitialAd.Show(onOpenCallback, onCloseCallback, onErrorCallback);
#endif
        }

        public void ShowRewardAd(Action onOpenCallback = null, Action onRewardedCallback = null, Action onCloseCallback = null, Action<string> onErrorCallback = null)
        {
#if YANDEX_GAMES
            YandexGames.VideoAd.Show(onOpenCallback, onRewardedCallback, onCloseCallback, onErrorCallback);
#endif
        }
    }
}
