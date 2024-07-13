using UnityEngine;

namespace Agava.Playground3D.Obby.Metrica
{
    public class CheckPointTrigger : MonoBehaviour
    {
        [SerializeField] private CheckPointsMetrica _metrica;

        private bool _active;
        
        private void OnValidate()
        {
            _metrica = FindObjectOfType<CheckPointsMetrica>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CheckPointTarget _) && _active == false)
            {
                _metrica.Send();
                _active = true;
            }
        }
    }
}
