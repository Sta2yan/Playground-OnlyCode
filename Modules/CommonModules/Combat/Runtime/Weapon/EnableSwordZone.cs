using UnityEngine;

namespace Agava.Combat
{
    public class EnableSwordZone : MonoBehaviour
    {
        [SerializeField] private Sword _sword;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TargetToEnableSword _))
            {
                _sword.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TargetToEnableSword _))
            {
                _sword.gameObject.SetActive(false);
            }
        }
    }
}
