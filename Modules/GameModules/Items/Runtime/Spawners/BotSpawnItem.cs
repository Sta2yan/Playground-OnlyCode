using UnityEngine;

namespace Agava.Playground3D.Items
{
    [CreateAssetMenu(menuName = "Items/Type/Create BotSpawnItem", fileName = "BotSpawnItem", order = 56)]
    public class BotSpawnItem : Item, IBotSpawn
    {
        [field: Header("Bot Information")]
        [field: SerializeField] public GameObject BotTemplate { get; private set; }
    }
}
