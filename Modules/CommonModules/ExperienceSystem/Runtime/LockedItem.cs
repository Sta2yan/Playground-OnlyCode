using UnityEngine;

namespace Agava.ExperienceSystem
{
    [CreateAssetMenu(fileName = "LockedItem", menuName = "LockedItems/Create LockedItem", order = 51)]
    public class LockedItem : ScriptableObject
    {
        [SerializeField] private UnlockedItemDescription _itemDescription;

        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Sprite UnlockedIcon { get; private set; }
        [field: SerializeField, Min(0)] public int UnlockingLevel { get; private set; } = 0;

        public string Description => _itemDescription.ToString();

        private bool _contentAssigment = false;

        public void Initialize(ILevelGateContent content)
        {
            _contentAssigment = content != null;
        }
    }

    internal enum UnlockedItemDescription
    {
        NewItem,
        NewMode
    }
}
