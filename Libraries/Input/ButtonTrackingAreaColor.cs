using UnityEngine;
using UnityEngine.UI;

namespace Agava.Input
{
    public class ButtonTrackingAreaColor : MonoBehaviour
    {
        [SerializeField] private ButtonTrackingArea _buttonTrackingArea;
        [SerializeField] private Color _clickSwitchColor;
        [SerializeField] private Image _image;
        [SerializeField] private bool _startStateEnabled;

        private Color _default;
        private bool _currentStateEnabled;

        private void Awake()
        {
            _currentStateEnabled = _startStateEnabled;
            _default = _image.color;

            if (_currentStateEnabled)
                _image.color = _clickSwitchColor;
        }

        private void Update()
        {
            if (_buttonTrackingArea.PastClickDisable == false)
                return;

            _currentStateEnabled = !_currentStateEnabled;
            _buttonTrackingArea.DisablePastClick();
            _image.color = _currentStateEnabled ? _clickSwitchColor : _default;
        }
    }
}
