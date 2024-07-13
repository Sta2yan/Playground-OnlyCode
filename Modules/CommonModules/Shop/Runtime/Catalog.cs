using Agava.ExperienceSystem;
using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Shop
{
    [CreateAssetMenu(menuName = "Shop/Create Catalog", fileName = "Catalog", order = 56)]
    internal class Catalog : ScriptableObject, ICatalog, ILevelGateContentContainer
    {
        [SerializeField, LeanTranslationName] private string _name;
        [SerializeField] private Product[] _products;
        [SerializeField] private Sprite _icon;

        public string Name => _name;
        public IProduct[] Products => _products;
        public Sprite Icon => _icon;

        public void ResetProducts()
        {
            foreach (var product in _products)
                product.Reset();
        }

        public bool TryUnlockContent(LockedItemsList lockedItemsList, int playerLevel, out List<ILevelGateContent> unlockedContent, bool instaUnlock=false)
        {
            unlockedContent = new();

            foreach (Product product in _products)
            {
                if (lockedItemsList.ItemExists(product))
                {
                    if (product.TryUnlock(playerLevel, instaUnlock))
                        unlockedContent.Add(product);
                }
            }

            return unlockedContent.Count > 0;
        }
    }
}
