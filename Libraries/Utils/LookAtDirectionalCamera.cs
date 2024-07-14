using UnityEngine;

namespace Agava.Utils
{
    public class LookAtDirectionalCamera
    {
        private readonly Camera _camera;
        
        public LookAtDirectionalCamera()
        {
            _camera = Camera.main;
        }
        
        public void Execute(Transform transform)
        {
            var cameraCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 50);
            var targetPosition = _camera.ScreenToWorldPoint(cameraCenter);
            transform.LookAt(targetPosition);
        }
    }
}
