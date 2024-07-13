using System;

namespace Agava.Inventory
{
    public interface IInventoryInput
    {
        event Action<int, int> Moved;
        event Action<int> Dropped;
    }
}
