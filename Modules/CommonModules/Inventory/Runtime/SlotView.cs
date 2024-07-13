using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Agava.Inventory
{
    public class SlotView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private const float DragDelay = .2f;

        [SerializeField] private Transform _content;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amountText;

        private Transform _dragContent;
        private int _index;
        private int _amount;

        private float _dragCountdown;

        public int Index => _index;

        public event Action<int, int> Moved;

        private void Update()
        {
            if (_dragCountdown == 0.0f)
                return;

            _dragCountdown -= Time.deltaTime;

            if (_dragCountdown < 0.0f)
                _dragCountdown = 0.0f;
        }

        public void Initialize(Transform dragContent)
        {
            _dragContent = dragContent;
        }

        public void Render(Sprite icon, int amount, int index)
        {
            _amount = amount;
            _amountText.text = amount > 1 ? _amount.ToString() : "";
            _index = index;

            if (icon == null)
            {
                _icon.color = Color.clear;
                return;
            }

            _icon.color = Color.white;
            _icon.sprite = icon;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragCountdown = DragDelay;
        }

        private Vector2 _test;
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_amount == 0)
                return;

            if (_dragCountdown > 0.0f)
            {
                _test = eventData.position;
                return;
            }

            _content.position += new Vector3(_test.x - _content.position.x, _test.y - _content.position.y);

            _content.position += new Vector3(eventData.delta.x, eventData.delta.y);
            _content.SetParent(_dragContent);
            
            _test = _content.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_amount == 0)
                return;

            GameObject foundGameObject = eventData.pointerCurrentRaycast.gameObject;
            bool pointerOverGameObject = foundGameObject != null;

            if (_dragCountdown != 0)
            {
                Moved?.Invoke(_index, _index);
            }
            else if (pointerOverGameObject && foundGameObject.TryGetComponent(out SlotView slotView))
            {
                Moved?.Invoke(_index, slotView.Index);
            }
            else if (pointerOverGameObject && foundGameObject.TryGetComponent(out InventoryOpenButton inventoryOpenButton))
            {
                if (inventoryOpenButton.TryGetFirstFreeInventorySlot(out slotView))
                {
                    Moved?.Invoke(_index, slotView.Index);
                }
                else
                {
                    Moved?.Invoke(_index, -1);
                }
            }
            else
            {
                Moved?.Invoke(_index, -1);
            }

            _content.SetParent(transform);
            _content.position = transform.position;
            _dragCountdown = 0.0f;
            _test = Vector2.zero;
        }
    }
}
