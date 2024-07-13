#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using TMPro;
using UnityEngine;

namespace Agava.Leaderboard
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        
#if YANDEX_GAMES
        public void Render(LeaderboardEntryResponse entry)
        {
            _rank.text = entry.rank.ToString();

            string name = entry.player.publicName;
            
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";

            _name.text = name;
            _score.text = entry.score.ToString();
        }
#endif
    }
}
