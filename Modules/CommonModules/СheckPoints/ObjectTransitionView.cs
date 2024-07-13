using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
#if YANDEX_GAMES
using Agava.Advertisement;
#endif

namespace Agava.CheckPoints
{
    public class ObjectTransitionView : MonoBehaviour
    {
        [SerializeField] private PointsContainer _pointsContainer;
        [SerializeField] private Button _executeButton;
        [SerializeField] private Transform _object;
        [SerializeField] private CanvasGroup _buttonCanvasGroup;


        private float _activationOffset = 10f;
        private bool _isPulseActivated = false;
        private Advertisement.Advertisement _advertisement;
        private float _duration = 0.5f;
        private float _multiplier = 0.8f;
        Sequence _sequence;

        private void OnEnable()
        {
            _executeButton.onClick.AddListener(OnExecuteButtonClick);
        }

        private void OnDisable()
        {
            _executeButton.onClick.RemoveListener(OnExecuteButtonClick);
        }

        private void Update()
        {
            //_buttonCanvasGroup.alpha = _pointsContainer.CanDisplaceObjectToPoint ? 1 : 0;
            if(_pointsContainer.TryDisplaceObjectToPoint(out Vector3 pointPosition) == false)
            {
                _buttonCanvasGroup.alpha = 0;
                return;
            }
           
            _buttonCanvasGroup.alpha = 1;
            if (pointPosition.y - _object.position.y >= _activationOffset)
                ActivatePulse();
            else
                DeactivatePulse();


        }

        private void DeactivatePulse()
        {
            _isPulseActivated = false;

            if (_sequence == null)
                return;

            _sequence.Kill();
            transform.DOScale(Vector3.one, _duration);
        }

        private void ActivatePulse()
        {
            if (_isPulseActivated == true)
                return;

            _isPulseActivated = true;
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(Vector3.one * _multiplier, _duration).SetEase(Ease.InBack).SetLoops(20, LoopType.Yoyo));
        }

        public void Initialize(Advertisement.Advertisement advertisement)
        {
            _advertisement = advertisement;
        }
        
        private void OnExecuteButtonClick()
        {
            _advertisement.ShowRewardAd(onCloseCallback: () => _pointsContainer.DisplaceToLastCompletePoint(_object, Vector3.up * 2));
        }
    }
}
