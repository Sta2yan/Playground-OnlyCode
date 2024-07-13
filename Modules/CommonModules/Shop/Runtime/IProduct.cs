using UnityEngine;

namespace Agava.Shop
{
    public interface IProduct
    {
        Sprite Icon { get; }
        string Id { get; }
        string TranslationId { get; }
        string PriceItemId { get; }
        Sprite PriceItemIcon { get; }
        int PriceItemCount { get; }
        int Count { get; }
        bool CanBuy { get; }
        bool Single { get; }
        bool DebugProduct { get; }

        void Buy();
        void Reset();
    }
}
