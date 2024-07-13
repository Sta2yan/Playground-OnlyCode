using UnityEngine;
using System.Linq;

namespace Agava.ExperienceSystem
{
    [CreateAssetMenu(fileName = "LockedItemsList", menuName = "LockedItems/Create LockedItemsList", order = 52)]
    public class LockedItemsList : ScriptableObject
    {
        [SerializeField] private LockedItem[] _items;

        public bool TryGetItemsByLevel(int level, out LockedItem[] items)
        {
            items = _items.Where(item => item.UnlockingLevel == level).ToArray();
            return items.Length > 0;
        }

        public bool ItemExists(ILevelGateContent content)
        {
            return _items.Any(item => item.Id == content.LockedItemId);
        }
    }
}
