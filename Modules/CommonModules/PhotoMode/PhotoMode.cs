using Agava.Customization;
using UnityEngine;

namespace Agava.PhotoMode
{
    public class PhotoMode : MonoBehaviour
    {
        [SerializeField] private GameObject _panelUI;
        [SerializeField] private GameObject _mobileUI;
        [SerializeField] private GameObject _characterModel;
        [SerializeField] private SkinList[] _skinLists;

        [Header("Controls")]
        [SerializeField] private KeyCode _hideUI;
        [SerializeField] private KeyCode _hideMobileUI;
        [SerializeField] private KeyCode _stopTime;
        [SerializeField] private KeyCode _hideCharacterModel;
        [SerializeField] private KeyCode _randomizeSkin;

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(_hideUI))
            {
                _panelUI.SetActive(!_panelUI.activeSelf);
            }

            if (Input.GetKeyDown(_hideMobileUI))
            {
                _mobileUI.SetActive(!_mobileUI.activeSelf);
            }

            if (Input.GetKeyDown(_stopTime))
            {
                Time.timeScale = 1 - Time.timeScale;
            }

            if (Input.GetKeyDown(_hideCharacterModel))
            {
                _characterModel.SetActive(!_characterModel.activeSelf);
            }

            if (Input.GetKeyDown(_randomizeSkin))
            {
                foreach(var skinList in _skinLists)
                {
                    skinList.ChooseRandomSkin();
                }
            }
#endif
        }
    }
}
