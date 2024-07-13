using System;

namespace Agava.Inventory
{
    public interface IInventoryView
    {
        event Action<int, int> Moved;
        event Action<int> Dropped;

        bool Opened { get; }

        void Render(ISlot[] slots, int selectedSlotIndex);
        void ActivateSlotsPanel();
        void DisableSlotsPanel();
    }
}
