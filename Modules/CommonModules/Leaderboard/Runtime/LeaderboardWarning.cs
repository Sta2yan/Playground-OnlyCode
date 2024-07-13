using System;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Leaderboard
{
    public class LeaderboardWarning : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _authorizeButton;

        public event Action Authorized;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
            _authorizeButton.onClick.AddListener(Authorize);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
            _authorizeButton.onClick.RemoveListener(Authorize);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void Authorize()
        {
#if YANDEX_GAMES
            PlayerAccount.Authorize(onSuccessCallback: () => Authorized?.Invoke());
#endif
        }
    }
}