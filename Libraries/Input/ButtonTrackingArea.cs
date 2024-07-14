using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Input
{
    public class ButtonTrackingArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public bool Pressed { get; private set; }
        public bool Clicked { get; private set; }
        public bool PastClickDisable { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked = true;
        }

        public void DisableClick()
        {
            Clicked = false;
            PastClickDisable = true;
        }

        public void DisablePastClick()
        {
            PastClickDisable = false;
        }

        

    }
}
