using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agava.MetricaTimer;
using Agava.Save;
using Agava.Levels;

namespace Agava.Playground3D.YandexMetrica
{
    public class MetricaSource : MonoBehaviour
    {
        [Header("Win buttons")]
        [SerializeField] private List<Button> _bedWarsWinLevelButtons;
        [SerializeField] private List<Button> _obbyWinLevelButtons;
        [SerializeField] private Button _onlyUpWinLevelButton;

        [Header("Timers")]
        [SerializeField] private LevelTimer _levelTimer;
        [SerializeField] private TotalTimer _totalTimer;

        [Header("Level loading")]
        [SerializeField] private string _firstTimePlayKey;
        [SerializeField] private LevelList _levelList;

        private bool _loading;
        private IEnumerable<IMetricaTarget> _metricaTargets;

        private void OnEnable()
        {
            if (_bedWarsWinLevelButtons != null)
                foreach (var bedWarsWinLevelButton in _bedWarsWinLevelButtons)
                    bedWarsWinLevelButton.onClick.AddListener(OnWinBedWarsLevelButtonClick);

            if (_obbyWinLevelButtons != null)
                foreach (var obbyWinLevelButton in _obbyWinLevelButtons)
                    obbyWinLevelButton.onClick.AddListener(OnWinObbyLevelButtonClick);

            if (_onlyUpWinLevelButton != null)
                _onlyUpWinLevelButton.onClick.AddListener(OnWinOnlyUpLevelButtonClick);
        }

        private void OnDisable()
        {
            if (_bedWarsWinLevelButtons != null)
                foreach (var bedWarsWinLevelButton in _bedWarsWinLevelButtons)
                    bedWarsWinLevelButton.onClick.RemoveListener(OnWinBedWarsLevelButtonClick);

            if (_obbyWinLevelButtons != null)
                foreach (var obbyWinLevelButton in _obbyWinLevelButtons)
                    obbyWinLevelButton.onClick.RemoveListener(OnWinObbyLevelButtonClick);

            if (_onlyUpWinLevelButton != null)
                _onlyUpWinLevelButton.onClick.RemoveListener(OnWinOnlyUpLevelButtonClick);
        }

        private void Update()
        {
            if (_levelTimer != null)
            {
                if (_levelTimer.Checked == false)
                {
                    Send(new MetricaEvent(EventType.MinutesSpent, _levelTimer.LevelName, new Dictionary<string, object>() { { "Time", _levelTimer.Minutes } }), true);
                    _levelTimer.Check();
                }
            }

            if (_totalTimer != null)
            {
                if (_totalTimer.Checked == false)
                {
                    Send(new MetricaEvent(EventType.MinutesSpent, "Total", new Dictionary<string, object>() { { "Time", _totalTimer.Minutes } }), true);
                    _totalTimer.Check();
                }
            }

            bool loading = _levelList.LoadingOperation != null;

            if ((loading ^ _loading) == false)
                return;

            _loading = loading;

            if (_loading)
                SendLevelStartEvent(_levelList.LoadingLevel);
        }

        public void Initialize(IEnumerable<IMetricaTarget> metricaTargets)
        {
            if (_metricaTargets != null)
                throw new InvalidOperationException(nameof(_metricaTargets) + " cant be Initialized 2 times");

            _metricaTargets = metricaTargets;
        }

        public void Send(MetricaEvent eventId, bool withParameter = false)
        {
            foreach (IMetricaTarget metricaTarget in _metricaTargets)
                metricaTarget.Send(eventId, withParameter);
        }

        private void OnWinBedWarsLevelButtonClick()
        {
            SendLevelWinEvent(LevelsToLoad.BedWars);
        }

        private void OnWinObbyLevelButtonClick()
        {
            SendLevelWinEvent(LevelsToLoad.Obby);
        }

        private void OnWinOnlyUpLevelButtonClick()
        {
            SendLevelWinEvent(LevelsToLoad.OnlyUp);
        }

        private void SendLevelStartEvent(LevelsToLoad modeName)
        {
            Send(new MetricaEvent(EventType.LevelStart, "", new Dictionary<string, object> { { "LevelName", modeName.ToString() } }), false);

            if ((string.IsNullOrEmpty(_firstTimePlayKey) || SaveFacade.HasKey(_firstTimePlayKey)) == false)
            {
                Send(new MetricaEvent(EventType.FirstPlayMode, "", new Dictionary<string, object>() { { "ModeName", modeName.ToString() } }), true);
                SaveFacade.SetInt(_firstTimePlayKey, 1);
            }
        }

        private void SendLevelWinEvent(LevelsToLoad modeName)
        {
            Send(new MetricaEvent(EventType.LevelWin, "", new Dictionary<string, object> { { "LevelName", modeName.ToString() } }), false);
        }
    }
}
