using UnityEngine;
using UnityEngine.UI;

namespace Agava.Combat
{
    public class ApplyDamageEffectView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private HealthView _healthView;

        private Color _color;

        private void Awake()
        {
            _color = _image.color;
        }

        private void Update()
        {
            _color.a = 1 - _healthView.ProgressImageFill;
            _image.color = _color;
        }
    }
}
