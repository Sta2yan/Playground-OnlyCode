using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Agava.Input
{
    public class MobileInteractiveArea : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IPointerMoveHandler
    {
        [SerializeField] private RaycastTarget _raycastTarget;
        [SerializeField] private float _delayPressed;
        [SerializeField] private LayerMask _layerMask;

        private Vector2 _currentTouch;
        private Vector2 _pastTouch;
        
        private List<float> _doubleTouche = new();
        private float _startDoubleTouchPosition;
        private bool _doubleToucheActive;
        
        private Coroutine _checkPressed;
        private WaitForSeconds _delay;
        private int _movedCountStep;
        private float _startLoop;
        private bool _canClick;

        public Vector2 InputVector => _currentTouch - _pastTouch;
        public bool Clicked { get; private set; }
        public bool Pressed { get; private set; }
        public bool Active { get; private set; }
        public float Loop { get; private set; }

        private void Awake()
        {
            _delay = new WaitForSeconds(_delayPressed);
        }

        private void Update()
        {
            ControlCameraZoom();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_doubleToucheActive)
                return;
            
            _movedCountStep = 0;
            _canClick = true;
            Active = true;
            
            _currentTouch = eventData.position;
            _pastTouch = _currentTouch;

            if (_checkPressed != null)
                StopCoroutine(_checkPressed);
        
            _checkPressed = StartCoroutine(CheckPressed());
            StartCoroutine(PastTouchControl());
        }
        
        public void OnPointerMove(PointerEventData eventData)
        {
            if (_doubleToucheActive)
                return;
            
            _movedCountStep++;

            _currentTouch = (eventData.position - _currentTouch).magnitude < 150 ? eventData.position : _currentTouch;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
            Active = false;

            _currentTouch = Vector2.zero;
            _pastTouch = Vector2.zero;

            if (_checkPressed != null)
                StopCoroutine(_checkPressed);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_doubleToucheActive)
                return;
            
            if (_canClick == false)
                return;
            
            if (_movedCountStep > 2)
                return;
            
            _raycastTarget.raycastTarget = false;

            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, 100, _layerMask) == false)
            {
                Clicked = true;
                Invoke(nameof(DisableRaycastTarget), .1f);
                return;
            }

            Clicked = true;
            
            if (hitInfo.collider.TryGetComponent(out Button button))
            {
                button.onClick.Invoke();
            }
            
            Invoke(nameof(DisableRaycastTarget), .1f);
        }

        public void DisableRaycastTarget()
        {
            _raycastTarget.raycastTarget = true;
            Clicked = false;
        }

        private void ControlCameraZoom()
        {
            if (_doubleToucheActive == false)
            {
                Loop = 0;
            
                if (UnityEngine.Input.touchCount == 2)
                {
                    _doubleTouche = new List<float>();
            
                    for (int i = 0; i < UnityEngine.Input.touchCount; i++)
                    {
                        var touchPosition = UnityEngine.Input.GetTouch(i).position.x;
                
                        if (touchPosition > Screen.width / 3 && touchPosition < Screen.width - Screen.width / 3)
                            _doubleTouche.Add(touchPosition);
                    }

                    if (_doubleTouche.Count == 2)
                    {
                        _startDoubleTouchPosition = Mathf.Abs(_doubleTouche[0] - _doubleTouche[1]);
                        _doubleToucheActive = true;
                    }
                }
                
                return;
            }

            if (UnityEngine.Input.touchCount != 2)
            {
                _doubleToucheActive = false;
                return;
            }
            
            var firstTouchPosition = UnityEngine.Input.GetTouch(0).position.x;
            var secondTouchPosition = UnityEngine.Input.GetTouch(1).position.x;
            var distanceBetweenTouches = Mathf.Abs(firstTouchPosition - secondTouchPosition);
            
            if (_startDoubleTouchPosition > distanceBetweenTouches)
                Loop = 1;
            else if (_startDoubleTouchPosition < distanceBetweenTouches)
                Loop = -1;
            else
                Loop = 0;

            _startDoubleTouchPosition = distanceBetweenTouches;
        }
        
        private IEnumerator PastTouchControl()
        {
            while (Active)
            {
                yield return null;

                _pastTouch = _currentTouch;
            }
        }
        
        private IEnumerator CheckPressed()
        {
            yield return _delay;

            if (_movedCountStep >= 2) 
                yield break;
            
            _canClick = false;
            Pressed = true;
        }
    }
}
