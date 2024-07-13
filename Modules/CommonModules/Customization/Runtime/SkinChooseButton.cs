using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Agava.Audio;

namespace Agava.Customization
{
    public class SkinChooseButton : MonoBehaviour
    {
        [SerializeField] private Image _chooseButtonImage;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _chooseButton;
        [SerializeField] private TMP_Text _chooseText;
        [SerializeField] private TMP_Text _chosenText;
        [SerializeField] private Sprite _chosenSprite;

        private Sprite _defaultSprite;
        private Advertisement.Advertisement _advertisement;

        public event Action Clicked;

        private void Awake()
        {
            _defaultSprite = _chooseButtonImage.sprite;
        }

        private void OnEnable()
        {
            _chooseButton.onClick.AddListener(OnChooseButtonClick);
        }

        private void OnDisable()
        {
            _chooseButton.onClick.RemoveListener(OnChooseButtonClick);
        }

        public void Initialize(Advertisement.Advertisement advertisement)
        {
            _advertisement = advertisement;
        }

        public void Render(bool chosen)
        {
            _chooseButton.gameObject.SetActive(true);

            if (chosen)
                Select();
            else
                Unselect();
        }

        public void Hide()
        {
            _chooseButton.gameObject.SetActive(false);
        }

        private void OnChooseButtonClick()
        {
            ShowReward();
        }

        private void Select()
        {
            _chooseText.gameObject.SetActive(false);
            _icon.gameObject.SetActive(false);

            _chosenText.gameObject.SetActive(true);

            _chooseButtonImage.sprite = _chosenSprite;
            _chooseButton.interactable = false;
        }

        private void Unselect()
        {
            _chooseText.gameObject.SetActive(true);
            _icon.gameObject.SetActive(true);

            _chosenText.gameObject.SetActive(false);

            _chooseButtonImage.sprite = _defaultSprite;
            _chooseButton.interactable = true;
        }

        private void ShowReward()
        {
            _advertisement.ShowRewardAd(onRewardedCallback: () => Clicked?.Invoke());
        }
    }
}
