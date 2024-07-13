using UnityEngine;

namespace Agava.Trampoline
{
    internal class TrampolineTarget : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void ForceJump(Vector3 direction)
        {
            _rigidbody.AddForce(direction, ForceMode.Impulse);
        }
    }
}
