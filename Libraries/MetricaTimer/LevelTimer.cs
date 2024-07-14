using UnityEngine;
using Agava.Levels;

namespace Agava.MetricaTimer
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private LevelsToLoad _levelName;

        private Timer _timer;

        public bool Checked => _timer.Checked;
        public int Seconds => Mathf.RoundToInt(_timer.Seconds);
        public int Minutes => _timer.Minutes;
        public string LevelName => _levelName.ToString();

        private void Start()
        {
            _timer = new Timer();
            StartCoroutine(_timer.StartTimer());
        }

        public void Check()
        {
            _timer.Check();
        }
    }
}
