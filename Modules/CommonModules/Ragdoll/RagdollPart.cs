using UnityEngine;

namespace Agava.Ragdoll
{
    public class RagdollPart : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        public Vector3 WorldCenterOfMass => _rigidbody.worldCenterOfMass;
        public RagdollCharacter RagdollCharacter { get; private set; }

        public void Initialize(RagdollCharacter ragdollCharacter)
        {
            RagdollCharacter = ragdollCharacter;

        }

        public void FreezePosition()
        {
            //_rigidbody.useGravity = false;
            //_rigidbody.isKinematic = true;
        }

        public void Enable()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;

            _collider.enabled = true;
        }

        public void Disable()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }
    }
}
