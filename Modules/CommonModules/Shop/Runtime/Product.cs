using Agava.ExperienceSystem;
using Agava.Playground3D.Items;
using System;
using UnityEngine;

namespace Agava.Shop
{
    [Serializable]
    internal class Product : IProduct, ILevelGateContent
    {
        [Header("Item")]
        [SerializeField] private Item _item;
        [SerializeField] private int _count;

        [Header("Price")]
        [SerializeField] private Item _priceItem;
        [SerializeField] private int _priceItemCount;
        [SerializeField] private bool _single;

        [Header("Level gating")]
        [SerializeField] private LockedItem _lockedItem;

        private bool _canBuy = true;

        public bool LevelGated => _lockedItem != null;
        public string LockedItemId => LevelGated ? _lockedItem.Id : null;
        public int UnlockingLevel => LevelGated ? _lockedItem.UnlockingLevel : 0;
        public bool Unlocked { get; private set; } = true;
        public Sprite UnlockingIcon => Icon;
        public Sprite Icon => _item.Icon;
        public string Id => _item.Id;
        public string TranslationId => _item.TranslationId;
        public string PriceItemId => _priceItem.Id;
        public Sprite PriceItemIcon => _priceItem.Icon;
        public int PriceItemCount => _priceItemCount;
        public int Count => _count;
        public bool Single => _single;
        public bool DebugProduct => _item.DebugItem;

        public bool CanBuy => _canBuy && Unlocked;

        public void Buy()
        {
            if (CanBuy == false)
                throw new InvalidOperationException();

            if (_single)
                _canBuy = false;
        }

        public void Reset()
        {
            _canBuy = true;
        }

        public bool TryUnlock(int currentLevel, bool instaUnlock = false)
        {
            Unlocked = currentLevel >= UnlockingLevel || (LevelGated == false) || instaUnlock;

            return Unlocked;
        }
    }
}