using UnityEngine;

namespace Agava.Combat
{
    internal class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICombatCharacter target))
                target.Hit(int.MaxValue, Vector3.zero);
        }
    }
}
