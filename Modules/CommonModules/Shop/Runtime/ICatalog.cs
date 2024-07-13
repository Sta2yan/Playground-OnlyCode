using UnityEngine;

namespace Agava.Shop
{
    public interface ICatalog
    {
        string Name { get; }
        IProduct[] Products { get; }
        Sprite Icon { get; }

        void ResetProducts();
    }
}
