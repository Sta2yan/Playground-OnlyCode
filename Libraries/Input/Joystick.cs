using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Input
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const float InputMaximum = 1f;
        private const float Double = 2f;

        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _stick;
        [SerializeField] private RectTransform _area;

        private Vector2 _backgroundStartPosition;
        private int _touchId;
        
        private bool _active;
        
        public Vector2 InputVector { get; private set; }

        private void Start()
        {
            _backgroundStartPosition = _background.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _active = true;

#if !UNITY_EDITOR
      _touchId = UnityEngine.Input.GetTouch(UnityEngine.Input.touchCount - 1).fingerId;      
#endif
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _background.anchoredPosition = _backgroundStartPosition;
            _stick.anchoredPosition = Vector2.zero;
            InputVector = Vector2.zero;

            _active = false;
        }

        private void Update()
        {
            if (_active == false)
                return;

            Vector2 position = Vector2.zero;
            
            #if UNITY_EDITOR
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, UnityEngine.Input.mousePosition, null,
                out position) == false) 
                return; 
            #else
            for (int i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                Touch touch = UnityEngine.Input.GetTouch(i);
                
                if (touch.fingerId == _touchId)
                {
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, touch.position, null,
                        out position) == false) 
                        return;       
                }
            }
            #endif

            position.x = (position.x * Double / _background.sizeDelta.x);
            position.y = (position.y * Double / _background.sizeDelta.y);

            InputVector = position;
            InputVector = (InputVector.magnitude > InputMaximum) ? InputVector.normalized : InputVector;

            _stick.anchoredPosition = new Vector2(InputVector.x * (_background.sizeDelta.x / Double),
                                                  InputVector.y * (_background.sizeDelta.y / Double));
        }
    }
}
