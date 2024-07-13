using Agava.Save;
using UnityEngine;

namespace Agava.Leaderboard
{
    public static class LeaderboardSettings
    {
        private const string LeaderboardSaveKey = "LeaderboardSaveKey";
        
        public static readonly string LeaderboardName = "RobloxLeaderboard";
        public static readonly int TopCount = 10;

        public static void AddScore(int value)
        {
#if YANDEX_GAMES
            int currentScore = SaveFacade.GetInt(LeaderboardSaveKey, 0);
            
            SaveFacade.SetInt(LeaderboardSaveKey, currentScore + value);
            YandexGames.Leaderboard.SetScore(LeaderboardName, currentScore + value);
#endif
        }
    }
}