using System.Collections.Generic;
using UnityEngine;

namespace Agava.Combat
{
    public class TeamViewResize : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> _objects;
        [SerializeField] private GameObject _content;
        [SerializeField] private float _offset;

        private void Awake()
        {
            var contents = _content.GetComponentsInChildren<Transform>();

            for (int i = 0; i < contents.Length - contents.Length / 2 - 2; i++)
                foreach (var rectTransform in _objects)
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + _offset,
                        rectTransform.anchoredPosition.y);
        }
    }
}
