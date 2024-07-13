using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Customization
{
    internal class SkinSelect : MonoBehaviour
    {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private SkinChooseButton _skinChooseButton;

        private SkinList _currentSkinList;

        public bool Showed => _currentSkinList != null;

        public void Show(SkinList skinList)
        {
            if (_currentSkinList != null)
                throw new InvalidOperationException("Already showing");
            
            _currentSkinList = skinList;
            
            _nextButton.onClick.AddListener(OnNextButtonClick);
            _previousButton.onClick.AddListener(OnPreviousButtonClick);
            _skinChooseButton.Clicked += OnChooseButtonClicked;
            
            _nextButton.gameObject.SetActive(true);
            _previousButton.gameObject.SetActive(true);
            
            _skinChooseButton.Render(_currentSkinList.CurrentSkinChosen);
        }

        public void Hide()
        {
            _currentSkinList?.EnableChosenSkin();
            _currentSkinList = null;
            
            _nextButton.onClick.RemoveListener(OnNextButtonClick);
            _previousButton.onClick.RemoveListener(OnPreviousButtonClick);
            _skinChooseButton.Clicked -= OnChooseButtonClicked;
            
            _nextButton.gameObject.SetActive(false);
            _previousButton.gameObject.SetActive(false);
            
            _skinChooseButton.Hide();
        }

        private void OnChooseButtonClicked()
        {
            _currentSkinList.ChoseCurrentSkin();
            _skinChooseButton.Render(_currentSkinList.CurrentSkinChosen);
        }

        private void OnNextButtonClick()
        {
            _currentSkinList.Next();
            _skinChooseButton.Render(_currentSkinList.CurrentSkinChosen);
        }

        private void OnPreviousButtonClick()
        {
            _currentSkinList.Previous();
            _skinChooseButton.Render(_currentSkinList.CurrentSkinChosen);
        }
    }
}
