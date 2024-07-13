using UnityEngine;
using UnityEngine.UI;

namespace Agava.Shop
{
    public class ShopTrigger : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private Button _openButton;
        
        public bool Entered { get; private set; }

        private void Awake()
        {
            _openButton.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ShopTriggerTarget _))
            {
                Entered = true;
                _openButton.gameObject.SetActive(true);
                _openButton.onClick.AddListener(OnOpenButtonClick);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ShopTriggerTarget _))
            {
                _shop.Close();
                Entered = false;
                _openButton.onClick.RemoveListener(OnOpenButtonClick);
                _openButton.gameObject.SetActive(false);
            }
        }

        private void OnOpenButtonClick()
        {
            _shop.Open();
        }
    }
}
