using System;
using System.Collections.Generic;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Agava.Leaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        private readonly List<LeaderboardEntryView> _views = new();

        [SerializeField] private LeaderboardEntryView _entryTemplate;
        [SerializeField] private Transform _container;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField, LeanTranslationName] private string _playerRankTranslateKey;
        [SerializeField, LeanTranslationName] private string _playerScoreTranslateKey;

        private void Awake()
        {
#if YANDEX_GAMES
            LoadPlayerEntry(new LeaderboardEntryResponse
            {
                score = 0,
                rank = 0
            });
#endif
        }

        private void OnEnable()
        {
#if YANDEX_GAMES
            YandexGames.Leaderboard.GetEntries(LeaderboardSettings.LeaderboardName, LoadEntries, null, LeaderboardSettings.TopCount);
            YandexGames.Leaderboard.GetPlayerEntry(LeaderboardSettings.LeaderboardName, LoadPlayerEntry);
#endif
        }

        private void OnDisable()
        {
            foreach (var view in _views)
                Destroy(view.gameObject);

            _views.Clear();
        }

#if YANDEX_GAMES
        private void LoadEntries(LeaderboardGetEntriesResponse result)
        {
            foreach (var entry in result.entries)
            {
                var entryView = Instantiate(_entryTemplate, _container);
                entryView.Render(entry);

                _views.Add(entryView);
            }
        }
#endif

#if YANDEX_GAMES
        private void LoadPlayerEntry(LeaderboardEntryResponse result)
        {
            string rank = LeanLocalization.GetTranslationText(_playerRankTranslateKey, _playerRankTranslateKey);
            string score = LeanLocalization.GetTranslationText(_playerScoreTranslateKey, _playerScoreTranslateKey);
            
            _playerRank.text = $"{rank}: {result.rank}";
            _playerScore.text = $"{score}: {result.score}";
        }
#endif
    }
}