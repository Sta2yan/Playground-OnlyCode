using UnityEngine;

namespace Agava.Utils
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private bool _X;
        [SerializeField] private bool _Y = true;
        [SerializeField] private bool _Z;
        [SerializeField] private float rotationSpeed = 10f;

        private void Update()
        {
            if (_Y)
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
            if (_Z)
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        
            if (_X)
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}
