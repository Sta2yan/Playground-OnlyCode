using UnityEngine;

namespace Agava.Utils
{
    public class FaceToCamera : MonoBehaviour
    {
        [SerializeField] private bool _needInvert = true;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.forward = _needInvert ? -_camera.transform.forward : _camera.transform.forward;
        }
    }
}
