using Agava.ExperienceSystem;
using Agava.Leaderboard;
using Agava.Time;
using UnityEngine;

namespace Agava.Playground3D.OnlyUp.Utils
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private TimeCountView _timer;
        [SerializeField] private GameObject _winPanel;

        private bool _triggered = false;

        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _playerWonEventRule;

        public void Initialize(ExperienceEventsContainer experienceEventsContainer, ExperienceEventRule playerWonEventRule)
        {
            _experienceEventsContainer = experienceEventsContainer;
            _playerWonEventRule = playerWonEventRule;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out FinishTarget _) && (_triggered == false))
            {
                _triggered = true;
                Execute();
            }
        }

        private void Execute()
        {
            _timer.Stop();
            _winPanel.SetActive(true);
            LeaderboardSettings.AddScore(30);

            _experienceEventsContainer.TriggerEvent(_playerWonEventRule.ExperienceEvent());
        }
    }
}
