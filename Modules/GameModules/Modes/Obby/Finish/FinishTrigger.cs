using Agava.ExperienceSystem;
using Agava.Save;
using Agava.Time;
using TMPro;
using UnityEngine;

namespace Agava.Playground3D.Obby.Finish
{
    public class FinishTrigger : MonoBehaviour
    {
        private const string BestTimeScoreKey = nameof(BestTimeScoreKey);
        private const int SecondsInMinute = 60;

        [SerializeField] private TimeCountView _timeCountView;
        [SerializeField] private GameObject _finishPanel;
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private TMP_Text _currentScore;

        private bool _triggered = false;

        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _playerWonEventRule;

        private void Awake()
        {
            _finishPanel.SetActive(false);
            UnityEngine.Time.timeScale = 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FinishTarget _) && (_triggered == false))
            {
                _triggered = true;
                Execute();
            }
        }

        public void Initialize(ExperienceEventsContainer experienceEventsContainer, ExperienceEventRule playerWonEventRule)
        {
            _experienceEventsContainer = experienceEventsContainer;
            _playerWonEventRule = playerWonEventRule;
        }

        private void Execute()
        {
            _timeCountView.Stop();

            if (SaveFacade.GetFloat(BestTimeScoreKey) < _timeCountView.Value)
                SaveFacade.SetFloat(BestTimeScoreKey, _timeCountView.Value);

            _bestScore.text = ConvertToTimeText(SaveFacade.GetFloat(BestTimeScoreKey));
            _currentScore.text = ConvertToTimeText(_timeCountView.Value);
            _finishPanel.SetActive(true);

            UnityEngine.Time.timeScale = 0;

# if !ANDROID_BUILD || UNITY_EDITOR
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
#endif

            _experienceEventsContainer.TriggerEvent(_playerWonEventRule.ExperienceEvent());
        }

        private string ConvertToTimeText(float value)
        {
            return $"{(int)value / SecondsInMinute:00}:{(int)value % SecondsInMinute:00}";
        }
    }
}
