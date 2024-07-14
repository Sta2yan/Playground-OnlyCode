using UnityEngine;

namespace Agava.Input
{
    public interface IInput
    {
        bool InventoryItemSelected(out int index);
        bool RotateDirection(out Vector2 direction);
        bool InteractiveShop();
        bool CloseInventory();
        bool OpenInventory();
        bool PressedAttack();
        bool RemoveBlock();
        bool PlaceBlock();
        bool DropItem();
        bool Sprint();
        bool Attack();
        bool Jump();
        bool Zoom();
        bool UseItem();
        float ChangeCameraDistance();
        Vector2 MoveDirection();
        Vector2 FirstPersonInput(bool touch);
        Vector2 ThirdPersonInput(bool touch);
    }
}
