using Agava.Audio;
using System;
using UnityEngine;

namespace Agava.Advertisement
{
    public class Advertisement
    {
        private readonly IAdvertisement _advertisement;

        private bool _active;
        
        public Advertisement(IAdvertisement advertisement)
        {
            _advertisement = advertisement;
        }

        public void ShowInterstitialAd(Action onOpenCallback = null, Action<bool> onCloseCallback = null, Action<string> onErrorCallback = null)
        {
            if (_active)
                return;

            Action newOnOpenCallback = () =>
            {
                _active = true;
                Time.timeScale = 0;
                PauseAudio.Pause();
                onOpenCallback?.Invoke();
            };

            Action<bool> newOnCloseCallback = (bool result) =>
            {
                _active = false;
                Time.timeScale = 1;
                PauseAudio.Unpause();
                onCloseCallback?.Invoke(result);
            };

            Action<string> newOnErrorCallback = (string error) =>
            {
                _active = false;
                Time.timeScale = 1;
                onErrorCallback?.Invoke(error);
            };

            _advertisement.ShowInterstitialAd(newOnOpenCallback, newOnCloseCallback, newOnErrorCallback);
        }

        public void ShowRewardAd(Action onOpenCallback = null, Action onRewardedCallback = null, Action onCloseCallback = null, Action<string> onErrorCallback = null)
        {
            if (_active)
                return;

            Action newOnOpenCallback = () =>
            {
                _active = true;
                Time.timeScale = 0;
                PauseAudio.Pause();
                onOpenCallback?.Invoke();
            };

            Action newOnCloseCallback = () =>
            {
                _active = false;
                Time.timeScale = 1;
                PauseAudio.Unpause();
                onCloseCallback?.Invoke();
            };

            Action<string> newOnErrorCallback = (string error) =>
            {
                _active = false;
                Time.timeScale = 1;
                onErrorCallback?.Invoke(error);
            };

            Action newOnRewardedCallback = () =>
            {
                _active = false;
                onRewardedCallback?.Invoke();
            };

            _advertisement.ShowRewardAd(newOnOpenCallback, newOnRewardedCallback, newOnCloseCallback, newOnErrorCallback);
        }
    }
}
