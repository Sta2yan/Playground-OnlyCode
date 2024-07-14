using System;

namespace Agava.Advertisement
{
    public interface IAdvertisement
    {
        void ShowInterstitialAd(Action onOpenCallback = null, Action<bool> onCloseCallback = null, Action<string> onErrorCallback = null);
        void ShowRewardAd(Action onOpenCallback = null, Action onRewardedCallback = null, Action onCloseCallback = null, Action<string> onErrorCallback = null);
    }
}
