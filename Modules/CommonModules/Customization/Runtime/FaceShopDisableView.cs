using UnityEngine;
using UnityEngine.UI;

namespace Agava.Customization
{
    internal class FaceShopDisableView : MonoBehaviour, ISkinCustomizationSelect
    {
        [SerializeField] private SkinList _skinListCharacter;
        [SerializeField] private SkinList _skinListFace;
        [SerializeField] private GameObject _faceShopPanel;
        [SerializeField] private GameObject _warningFaceShopPanel;
        [SerializeField] private Button _characterCustomization;

        private void Update()
        {
            if (_skinListCharacter.CurrentSkin.Without)
            {
                _warningFaceShopPanel.SetActive(true);
                _faceShopPanel.SetActive(false);
            }
            else
            {
                _warningFaceShopPanel.SetActive(false);
                _faceShopPanel.SetActive(true);
            }
        }

        public void Execute()
        {
            if (_skinListCharacter.CurrentSkin.Without)
            {
                _characterCustomization.onClick.Invoke();
                _skinListFace.DisableAll();
            }
        }
    }
}
