using UnityEngine;

namespace Agava.Playground3D.Items
{
    [CreateAssetMenu(menuName = "Items/Type/Create BlockItem", fileName = "BlockItem", order = 56)]
    public class BlockItem : Item, IBlock
    {
        [field: Header("Block Information")]
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public GameObject BlockTemplate { get; private set; }
    }
}