using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Agava.Utils
{
    public class ObjectMoveToMouse : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Image _image;

        private bool _isMobile;

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        private void Update()
        {
            if (_isMobile)
            {
                _gameObject.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            }
            else
            {
                _image.enabled = !EventSystem.current.MouseOverUI();
                _gameObject.transform.position = Input.mousePosition;
            }
        }

        public void Initialize(bool isMobile)
        {
            _isMobile = isMobile;
        }
    }
}
