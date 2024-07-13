using System;
using UnityEngine;

namespace Agava.Trampoline
{
    internal class TrampolineTrigger : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _jumpForce = 15f;

        public float CurrentJumpForce => _jumpForce;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TrampolineTarget target))
            {
                target.ForceJump(Vector3.up * _jumpForce);
            }
        }

        public void ChangeJumpForce(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            
            _jumpForce = value;
        }
    }
}
