using System;
using UnityEngine;

namespace Agava.Advertisement
{
    public class MockReward : IAdvertisement
    {
        public void ShowInterstitialAd(Action onOpenCallback = null, Action<bool> onCloseCallback = null, Action<string> onErrorCallback = null)
        {
            onOpenCallback?.Invoke();
            onCloseCallback?.Invoke(true);
            Debug.Log("Interstitial shown");
        }

        public void ShowRewardAd(Action onOpenCallback = null, Action onRewardedCallback = null, Action onCloseCallback = null, Action<string> onErrorCallback = null)
        {
            onOpenCallback?.Invoke();
            onRewardedCallback?.Invoke();
            onCloseCallback?.Invoke();
            Debug.Log("Reward shown");
        }
    }
}
