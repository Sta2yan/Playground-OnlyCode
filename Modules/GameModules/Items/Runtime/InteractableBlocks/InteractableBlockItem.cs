using UnityEngine;

namespace Agava.Playground3D.Items
{
    [CreateAssetMenu(menuName = "Items/Type/Create InteractableBlockItem", fileName = "InteractableBlockItem", order = 56)]
    public class InteractableBlockItem : BlockItem, IInteractableBlock
    {
        [field: Header("Additional")]
        [field: SerializeField] public bool TriggerCollider { get; private set; }
        [field: SerializeField] public GameObject InteractableObject { get; private set; }
    }
}
