using System.Collections;
using UnityEngine;

namespace Agava.MetricaTimer
{
    public class Timer
    {
        public Timer(float startTime)
        {
            Seconds = startTime;
        }

        public Timer() : this(0.0f) { }

        public bool Checked { get; private set; } = true;
        public int Minutes { get; private set; }
        public float Seconds { get; private set; }

        public void Check()
        {
            Checked = true;
        }

        public IEnumerator StartTimer()
        {
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            while (true)
            {
                Seconds += Time.unscaledDeltaTime;
                UpdateTimer();
                yield return waitForEndOfFrame;
            }
        }

        private void UpdateTimer()
        {
            if (Checked == false)
                return;

            int minutes = Mathf.FloorToInt(Seconds / 60);

            switch (minutes)
            {
                case 0: return;

                case <= 10:
                    {
                        if (minutes == Minutes)
                            return;

                        break;
                    }

                case <= 30:
                    {
                        if (minutes - Minutes < 5)
                            return;

                        break;
                    }

                case <= 60:
                    {
                        if (minutes - Minutes < 10)
                            return;

                        break;
                    }

                default:
                    {
                        if (minutes - Minutes < 60)
                            return;

                        break;
                    }
            }

            Minutes = minutes;
            Checked = false;
        }
    }
}
