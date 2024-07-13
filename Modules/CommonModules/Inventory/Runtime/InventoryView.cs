using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Inventory
{
    public class InventoryView : MonoBehaviour, IInventoryView, IInventoryInput
    {
        private const float Position = 2.508484f;
        private const float OffsetPosition = 10f;
        private const int MaxFastSlotsNumber = 9;

        [SerializeField] private GameObject _slotsPanel;
        [SerializeField] private Transform _dragContent;
        [SerializeField] private Button _openButton;
        [SerializeField] private List<Button> _closeButtons;
        [SerializeField] private SlotView[] _slotViews;
        
        [SerializeField] private List<TMP_Text> _fastSlotsNumberText;
        [SerializeField] private List<Image> _fastSlotsNumberImage;
        [SerializeField] private Sprite _selectFastSlotNumber;
        [SerializeField] private Sprite _defaultFastSlotNumber;

        public bool Opened { get; private set; }
        public int Slots => _slotViews.Length;
        
        public event Action<int, int> Moved;
        public event Action<int> Dropped;

        private void Awake()
        {
            DisableSlotsPanel();
            
            foreach (var view in _slotViews)
                view.Initialize(_dragContent);
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);

            foreach (var closeButton in _closeButtons) 
                closeButton.onClick.AddListener(OnCloseButtonClick);

            foreach (var view in _slotViews)
                view.Moved += OnItemMoved;
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
            
            foreach (var closeButton in _closeButtons) 
                closeButton.onClick.RemoveListener(OnCloseButtonClick);

            foreach (var view in _slotViews)
                view.Moved -= OnItemMoved;
        }

        public void Render(ISlot[] slots, int selectedSlotIndex)
        {
            for (int i = 0; i < _slotViews.Length; i++)
            {
                _slotViews[i].Render(slots[i].CurrentItem.Sprite, slots[i].ItemsCount, i);

                if (i < MaxFastSlotsNumber)
                {
                    _fastSlotsNumberText[i].color = Color.white;
                    _fastSlotsNumberImage[i].sprite = _defaultFastSlotNumber;
                    var rectTransform = _slotViews[i].transform as RectTransform;
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x , Position);   
                }
            }

            Select(selectedSlotIndex);
        }

        public void ActivateSlotsPanel()
        {
            _slotsPanel.SetActive(true);
            Opened = true;
        }

        public void DisableSlotsPanel()
        {
            _slotsPanel.SetActive(false);
            Opened = false;
        }
        
        private void Select(int index)
        {
            _fastSlotsNumberText[index].color = Color.black;
            _fastSlotsNumberImage[index].sprite = _selectFastSlotNumber;
            var rectTransform = _slotViews[index].transform as RectTransform;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x , OffsetPosition);
        }

        private void OnItemMoved(int indexFrom, int indexTo)
        {
            if (indexTo == -1)
                Dropped?.Invoke(indexFrom);
            else
                Moved?.Invoke(indexFrom, indexTo);
        }

        private void OnCloseButtonClick()
        {
            DisableSlotsPanel();
        }

        private void OnOpenButtonClick()
        {
            ActivateSlotsPanel();
        }
    }
}