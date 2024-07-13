using UnityEngine;

namespace Agava.Playground3D.Items
{
    public interface IInteractableBlock : IBlock
    {
        bool TriggerCollider { get; }
        GameObject InteractableObject { get; }
    }
}
