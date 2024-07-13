using Agava.Advertisement;
using Agava.MetricaTimer;
using TMPro;
using UnityEngine;
using Agava.Utils;

namespace Agava.Playground3D.CoffeeBreak
{
    public class CoffeeBreak : MonoBehaviour
    {
        [SerializeField] private Milk _milk;
        [SerializeField] private MonoBehaviour _conditionCheckObject;
        [SerializeField] private InterstitialLaunch _interstitialLaunch;
        [SerializeField] private LevelTimer _timer;

        [Header("Time")]
        [SerializeField] private int _secondsCooldown = 60;
        [SerializeField] private int _coffeeBreakDelaySeconds = 15;

        [Header("View")]
        [SerializeField] private GameObject _viewRoot;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private CoffeeBreakMiniGame _miniGame;

        private CoffeeBreakView _view;
        private int _lastInterstitialTime = 0;

        private IConditionCheck _conditionCheck => (IConditionCheck)_conditionCheckObject;

        public bool Active { get; private set; } = false;

        private void Awake()
        {
            _viewRoot.SetActive(false);
            _miniGame.StopMiniGame();
            _view = new CoffeeBreakView(this, _count);
        }

        private void OnValidate()
        {
            if (_conditionCheckObject && _conditionCheckObject is not IConditionCheck)
            {
                Debug.LogError(nameof(_conditionCheckObject) + " needs to implement " + nameof(IConditionCheck));
                _conditionCheckObject = null;
            }
        }

        private void Update()
        {
            if (_milk == null)
                return;

            if (Active)
                return;

            if (_conditionCheck != null)
            {
                if (_conditionCheck.ConditionMet == false)
                    return;
            }

            int timerSeconds = _timer.Seconds;

            if (timerSeconds - _lastInterstitialTime > _secondsCooldown)
            {
                _view.StartCoffeeBreak(_coffeeBreakDelaySeconds,
                    onTimeStart: () =>
                    {
                        Active = true;
                        Time.timeScale = 0;
                        _viewRoot.SetActive(true);
                        _miniGame.StartMiniGame();
                    },
                    onTimeEnd: () =>
                    {
                        Active = false;
                        Time.timeScale = 1;
                        _viewRoot.SetActive(false);
                        _miniGame.StopMiniGame();
                        StartCoroutine(_interstitialLaunch.Execute());
                    });

                _lastInterstitialTime = timerSeconds;
            }
        }

        public void Initialize(Milk milk)
        {
            _milk = milk;
        }
    }
}
