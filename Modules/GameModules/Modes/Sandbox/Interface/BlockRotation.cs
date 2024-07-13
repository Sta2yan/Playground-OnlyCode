using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.Sandbox.Interface
{
    public class BlockRotation : MonoBehaviour
    {
        [SerializeField] private Button _leftRotateButton;
        [SerializeField] private Button _rightRotateButton;

        private void OnEnable()
        {
            _leftRotateButton.onClick.AddListener(RotateLeft);
            _rightRotateButton.onClick.AddListener(RotateRight);
        }

        private void OnDisable()
        {
            _leftRotateButton.onClick.RemoveListener(RotateLeft);
            _rightRotateButton.onClick.RemoveListener(RotateRight);
        }

        private void RotateLeft()
        {

        }

        private void RotateRight()
        {

        }
    }
}
