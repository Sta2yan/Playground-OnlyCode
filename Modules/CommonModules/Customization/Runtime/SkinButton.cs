using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Customization
{
    internal class SkinButton : MonoBehaviour
    {
        [SerializeField] private SkinList _targetSkinList;
        [SerializeField] private Transform _cameraTargetPoint;
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _defaultButtonSprite;
        [SerializeField] private Sprite _selectedButtonSprite;
        [Header("Icon")]
        [SerializeField] private Image _icon;
        [SerializeField] private Color _defaultIconColor;
        [SerializeField] private Color _selectedIconColor;

        public event Action<SkinButton> Clicked;

        public SkinList TargetSkinList => _targetSkinList;
        public Transform CameraTargetPoint => _cameraTargetPoint;

        public void Show()
        {
            gameObject.SetActive(true);
            _button.onClick.AddListener(OnButtonClick);

            RenderUnselect();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _button.onClick.RemoveListener(OnButtonClick);
        }
        
        public void RenderSelect()
        {
            _button.image.sprite = _selectedButtonSprite;
            _icon.color = _selectedIconColor;
        }

        public void RenderUnselect()
        {
            _button.image.sprite = _defaultButtonSprite;
            _icon.color = _defaultIconColor;
        }
        
        private void OnButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}