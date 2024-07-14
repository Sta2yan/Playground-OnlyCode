using UnityEngine;
using UnityEngine.UI;

namespace Agava.Utils
{
    internal class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            ButtonClickSoundSource.Play();
        }
    }
}
