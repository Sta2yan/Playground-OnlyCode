using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.Dynamite
{
    internal class DynamiteActivateView : MonoBehaviour
    {
        [SerializeField] private Button _activeButton;
        [SerializeField] private Dynamite _dynamite;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private Image _icon;

        private float _currentTime;

        private void Awake()
        {
            _activeButton.gameObject.SetActive(false);
            _currentTime = _dynamite.ActivationDelay + 1;
            _time.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_dynamite.Active == false)
                return;

            _currentTime -= Time.deltaTime;

            _time.text = ((int)_currentTime).ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out DynamiteActivateViewTarget _))
            {
                _activeButton.gameObject.SetActive(true);
                _activeButton.onClick.AddListener(OnActiveButtonClick);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out DynamiteActivateViewTarget _))
            {
                if (_dynamite.Active == false)
                    _activeButton.gameObject.SetActive(false);

                _activeButton.onClick.RemoveListener(OnActiveButtonClick);
            }
        }

        private void OnActiveButtonClick()
        {
            _activeButton.interactable = false;
            _dynamite.Activate(_dynamite.ActivationDelay);
            _time.gameObject.SetActive(true);
            _icon.gameObject.SetActive(false);
        }
    }
}
