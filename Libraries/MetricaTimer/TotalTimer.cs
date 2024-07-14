using UnityEngine;
using Agava.Save;

namespace Agava.MetricaTimer
{
    public class TotalTimer : MonoBehaviour
    {
        [SerializeField] private string _timeSaveKey;

        private static Timer _timer;
        private static Coroutine _timeCoroutine;

        public bool Checked => _timer.Checked;
        public int Minutes => _timer.Minutes;

        private void Start()
        {
            if (_timer == null)
            {
                if (SaveFacade.HasKey(_timeSaveKey))
                {
                    _timer = new Timer(SaveFacade.GetFloat(_timeSaveKey));
                }
                else
                {
                    _timer = new Timer();
                }
            }

            if (_timeCoroutine != null)
                StopCoroutine(_timeCoroutine);

            _timeCoroutine = StartCoroutine(_timer.StartTimer());
        }

        private void OnApplicationQuit()
        {
            SaveTime();
        }

        public void Check()
        {
            _timer.Check();
        }

        private void SaveTime()
        {
            SaveFacade.SetFloat(_timeSaveKey, _timer.Seconds);
        }
    }
}
