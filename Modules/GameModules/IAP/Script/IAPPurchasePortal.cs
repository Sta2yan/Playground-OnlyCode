using System;
using UnityEngine;
using UnityEngine.UI;
using Agava.Save;
using Agava.Playground3D.MainMenu;
using TMPro;

namespace Assets.Source.Plauground.IAp
{
    public class IAPPurchasePortal : MonoBehaviour
    {
        private const string _saveKey = "new_year_event";

        [SerializeField] private Button _buyButton;
        [SerializeField] private IApType _iApType;
        //[SerializeField] private IAPRoot _iAPRoot;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private PortalToMode _portalToMode;
        [SerializeField] private TMP_Text _text;

        private string _price;
        private IIApEntity _IiApEntity;

        public bool IsPurchased { get; private set; } = false;
        public IApType IApType => _iApType;
        public Action<IApType, Action> BuyClicked;

        private void OnEnable()
        {
            _portalToMode.PlayButtonClicked += OnPlayButtonClicked;
            _buyButton.onClick.AddListener(OnBuyButttonClicked);
            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _portalToMode.PlayButtonClicked += OnPlayButtonClicked;
            _buyButton.onClick.RemoveListener(OnBuyButttonClicked);
            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.RemoveListener(Hide);
        }

        private void Hide()
        {
            _panel.SetActive(false);
        }
        public void Show()
        {
            _panel.SetActive(true);
        }


        private void OnBuyButttonClicked()
        {

            Action onPurchaseCallback = () =>
            {
                print("Purchased");
                SaveFacade.SetInt(_saveKey, 1);
                _IiApEntity.SetBought();
                IsPurchased = _IiApEntity.Bought;
                Hide();

            };

            BuyClicked?.Invoke(_iApType, onPurchaseCallback);

            //_iAPRoot.IApPurchase.PurchaseProduct(_iApType, onPurchaseCallback);
        }

        private void OnPlayButtonClicked()
        {
            if (IsPurchased)
                _portalToMode.LoadPurchasedLevel();
            else
                Show();
        }

        public void Init(IIApEntity iiApEntity)
        {
            _IiApEntity = iiApEntity;
            _portalToMode.SetIAP();
            if(SaveFacade.HasKey(_saveKey))
            {
                _IiApEntity.SetBought();
                IsPurchased = iiApEntity.Bought;
                return;
            }

            
            _price = iiApEntity.PriceValue;
            _text.text = _text.text + _price + "$";
        }

        // Action onPurchaseCallback = () =>
        //{
        //    print("Purchased");
        //    SaveFacade.SetInt(_saveKey, 1);
        //    Show();

        //};



    }
}
