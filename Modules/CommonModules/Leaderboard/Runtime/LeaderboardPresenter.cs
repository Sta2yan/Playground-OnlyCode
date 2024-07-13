#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Leaderboard
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _leaderboardRoot;
        [SerializeField] private LeaderboardWarning _warning;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            _leaderboardRoot.SetActive(false);
            _warning.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OpenLeaderboard);
            _closeButton.onClick.AddListener(CloseLeaderBoard);
            _warning.Authorized += OnAuthorized;
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OpenLeaderboard);
            _closeButton.onClick.RemoveListener(CloseLeaderBoard);
            _warning.Authorized -= OnAuthorized;
        }

        private void OpenLeaderboard()
        {
#if UNITY_EDITOR
            return;
#endif

#if YANDEX_GAMES
            if (PlayerAccount.IsAuthorized == false)
                _warning.gameObject.SetActive(true);
            else
                _leaderboardRoot.SetActive(true);      
#endif
        }

        private void CloseLeaderBoard()
        {
#if UNITY_EDITOR
            return;
#endif

            _leaderboardRoot.SetActive(false);
        }

        private void OnAuthorized()
        {
            _warning.gameObject.SetActive(false);
            _leaderboardRoot.SetActive(true);
        }
    }
}