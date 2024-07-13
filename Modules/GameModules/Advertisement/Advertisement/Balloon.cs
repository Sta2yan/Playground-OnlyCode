using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Agava.Playground3D.CoffeeBreak
{
    public class Balloon : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private Vector3 _localStartPosition;
        private Tween _animation;

        private Action _onClick;

        private void Awake()
        {
            _localStartPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke();
            Hide();
        }

        public void StartAnimation(Vector3 endPoint, Action onClick = null)
        {
            _onClick = onClick;
            transform.localPosition = _localStartPosition;
            _animation = transform
                .DOMoveY(endPoint.y, UnityEngine.Random.Range(2.2f, 5f))
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnStepComplete(Show)
                .SetLoops(-1, LoopType.Restart);
        }

        public void Show()
        {
            _image.enabled = true;
            _button.interactable = true;
        }

        public void StopAnimation()
        {
            if (_animation != null)
                _animation.Kill();
        }

        public void Hide()
        {
            _image.enabled = false;
            _button.interactable = false;
        }
    }
}
