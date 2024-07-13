using UnityEngine;

namespace Agava.Ragdoll
{
    public interface IRagdollCharacter
    {
        void Enable(int attackLayers);
        void Disable();
    }
}
