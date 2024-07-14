using System;
using CAS;

namespace Agava.Advertisement
{
    public class CASReward : IAdvertisement
    {
        private readonly IMediationManager _manager;

        public CASReward()
        {
            _manager = MobileAds.BuildManager().Build();
        }

        private event Action OnOpenInterstitialCallback;
        private event Action OnCloseRewardCallback;
        private event Action OnRewardedAdCompletedCallback;
        private event Action<bool> OnCloseInterstitialCallback;
        private event Action OnOpenRewardCallback;

        public void ShowInterstitialAd(Action onOpenCallback = null, Action<bool> onCloseCallback = null, Action<string> onErrorCallback = null)
        {
            _manager.OnInterstitialAdShown += OnInterstitialAdShown;
            _manager.OnInterstitialAdClosed += OnInterstitialAdClosed;

            OnOpenInterstitialCallback = onOpenCallback;
            OnCloseInterstitialCallback = onCloseCallback;

            _manager.ShowAd(AdType.Interstitial);
        }

        public void ShowRewardAd(Action onOpenCallback = null, Action onRewardedCallback = null, Action onCloseCallback = null,
            Action<string> onErrorCallback = null)
        {
            _manager.OnRewardedAdShown += OnRewardAdShown;
            _manager.OnRewardedAdCompleted += OnRewardAdCompleted;
            _manager.OnRewardedAdClosed += OnRewardAsClosed;
            
            OnOpenRewardCallback = onOpenCallback;
            OnRewardedAdCompletedCallback = onRewardedCallback;
            OnCloseRewardCallback = onCloseCallback;
            
            _manager.ShowAd(AdType.Rewarded);
        }

        private void OnInterstitialAdShown()
        {
            OnOpenInterstitialCallback?.Invoke();
            _manager.OnInterstitialAdShown -= OnInterstitialAdShown;
        }

        private void OnInterstitialAdClosed()
        {
            OnCloseInterstitialCallback?.Invoke(true);
            _manager.OnInterstitialAdClosed -= OnInterstitialAdClosed;
        }

        private void OnRewardAdShown()
        {
            OnOpenRewardCallback?.Invoke();
            _manager.OnRewardedAdShown -= OnRewardAdShown;
        }

        private void OnRewardAdCompleted()
        {
            OnRewardedAdCompletedCallback?.Invoke();
            _manager.OnRewardedAdCompleted -= OnRewardAdCompleted;
 
        }

        private void OnRewardAsClosed()
        {
            OnCloseRewardCallback?.Invoke();
            _manager.OnRewardedAdClosed -= OnRewardAsClosed;
        }
    }
}
