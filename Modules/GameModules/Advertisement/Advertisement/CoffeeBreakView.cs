using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Agava.Playground3D.CoffeeBreak
{
    public class CoffeeBreakView
    {
        private readonly MonoBehaviour _root;
        private readonly TMP_Text _count;

        private Coroutine _coroutine;

        public CoffeeBreakView(MonoBehaviour root, TMP_Text count)
        {
            _root = root;
            _count = count;
        }

        public void StartCoffeeBreak(int countDownTime, Action onTimeStart, Action onTimeEnd)
        {
            if (_coroutine != null)
                _root.StopCoroutine(_coroutine);

            _coroutine = _root.StartCoroutine(CountTime(countDownTime, onTimeStart, onTimeEnd));
        }

        private IEnumerator CountTime(float time, Action onTimeStart, Action onTimeEnd)
        {
            onTimeStart?.Invoke();
            float timeLeft = time;

            while (timeLeft > 0)
            {
                timeLeft -= Time.unscaledDeltaTime;

                if (timeLeft < 0)
                    timeLeft = 0;

                _count.text = $"{timeLeft:0}";
                yield return null;
            }

            onTimeEnd?.Invoke();
            _coroutine = null;
        }
    }
}
