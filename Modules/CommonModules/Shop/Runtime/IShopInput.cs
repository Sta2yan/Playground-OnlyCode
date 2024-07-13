using System;

namespace Agava.Shop
{
    internal interface IShopInput
    {
        event Action<IProduct> Changed;
    }
}
