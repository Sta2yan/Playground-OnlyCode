using UnityEngine;

namespace Agava.Playground3D.Items
{
    [CreateAssetMenu(menuName = "Items/Type/Create PickItem", fileName = "PickItem", order = 56)]
    public class PickItem : Item, IPick
    {
        [field: Header("Pick Information")]
        [field: SerializeField] public float DigDelay { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
