using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Utils
{
    public static class EventSystemExtensions
    {
        private static readonly LayerMask UILayer = LayerMask.NameToLayer("UI");

        public static bool MouseOverUI(this EventSystem eventSystem)
        {
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            eventSystem.RaycastAll(eventData, raysastResults);

            for (int index = 0; index < raysastResults.Count; index++)
            {
                if (raysastResults[index].gameObject.layer == UILayer)
                    return true;
            }

            return false;
        }
    }
}
