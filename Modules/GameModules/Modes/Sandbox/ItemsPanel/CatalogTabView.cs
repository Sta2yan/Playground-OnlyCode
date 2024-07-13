using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.Sandbox.ItemsPanel
{
    public class CatalogTabView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image[] _background;
        [SerializeField] private Color _unselectedColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Button[] _buttons;

        public bool Selected { get; private set; } = false;

        private void OnEnable()
        {
            foreach(Button button in _buttons)
                button.onClick.AddListener(OnSelected);
        }

        private void OnDisable()
        {
            foreach (Button button in _buttons)
                button.onClick.RemoveListener(OnSelected);
        }

        private void OnValidate()
        {
            Unselect();
        }

        public void Render(Sprite iconSprite)
        {
            _icon.sprite = iconSprite;
        }

        public void Select()
        {
            Selected = true;
            ChangeBackgroundColor(_selectedColor);
        }

        public void Unselect()
        {
            Selected = false;
            ChangeBackgroundColor(_unselectedColor);
        }

        private void OnSelected()
        {
            Selected = true;
        }

        private void ChangeBackgroundColor(Color color)
        {
            foreach (Image image in _background)
                image.color = color;
        }
    }
}
