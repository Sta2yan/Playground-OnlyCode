using Agava.Ragdoll;
using UnityEngine;

namespace Agava.Playground3D.GravyGun
{
    public class RagdollManipulator : MonoBehaviour
    {
        private const float MaxDistance = 10000;

        [SerializeField] private LayerMask _charactersMask;

        public void Use()
        {
            if (TryGetTargetCharacter(out var ragdollCharacter))
            {
                if (ragdollCharacter.RagdollEnabled)
                {
                    ragdollCharacter.Disable();
                    Debug.Log($"Регдолл выключен на объекте: {ragdollCharacter.name}");
                }
                else
                {
                    ragdollCharacter.Enable(0);
                    Debug.Log($"Регдолл включен на объекте: {ragdollCharacter.name}");
                }
            }
        }

        private bool TryGetTargetCharacter(out RagdollCharacter ragdollCharacter)
        {
            Ray ray = RayBeamDirection();

            ragdollCharacter = null;

            if (Physics.Raycast(ray, out var hit, MaxDistance, _charactersMask, QueryTriggerInteraction.Collide))
            {
                hit.collider.TryGetComponent(out ragdollCharacter);

                if (hit.collider.TryGetComponent(out RagdollPart ragdollPart))
                {
                    ragdollCharacter = ragdollPart.RagdollCharacter;
                }
            }

            return ragdollCharacter != null;
        }

        private Ray RayBeamDirection()
        {
            return Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
        }
    }
}
