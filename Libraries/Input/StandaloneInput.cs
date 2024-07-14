using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Agava.Utils;

namespace Agava.Input
{
    public class StandaloneInput : IInput
    {
        private const string MouseScrollWheel = "Mouse ScrollWheel";
        private const string MouseHorizontalAxis = "Mouse X";
        private const string MouseVerticalAxis = "Mouse Y";
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const int RightMouseButton = 1;
        private const int LeftMouseButton = 0;

        private readonly float _stepChangeDistanceCamera;

        private readonly Dictionary<KeyCode, int> _inventorySlotKeyCodes = new Dictionary<KeyCode, int>()
        {
            {KeyCode.Alpha1, 0},
            {KeyCode.Alpha2, 1},
            {KeyCode.Alpha3, 2},
            {KeyCode.Alpha4, 3},
            {KeyCode.Alpha5, 4},
            {KeyCode.Alpha6, 5},
            {KeyCode.Alpha7, 6},
            {KeyCode.Alpha8, 7},
            {KeyCode.Alpha9, 8}
        };

        private bool MouseOverUI => EventSystem.current.MouseOverUI();

        public StandaloneInput(float stepChangeDistanceCamera)
        {
            _stepChangeDistanceCamera = stepChangeDistanceCamera;
        }

        public bool PlaceBlock()
        {
            return UnityEngine.Input.GetMouseButtonDown(LeftMouseButton) && !MouseOverUI;
        }

        public bool InteractiveShop()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.F);
        }

        public Vector2 MoveDirection()
        {
            return new Vector2(UnityEngine.Input.GetAxisRaw(HorizontalAxis), UnityEngine.Input.GetAxisRaw(VerticalAxis));
        }

        public bool RotateDirection(out Vector2 direction)
        {
            direction = new Vector2(UnityEngine.Input.GetAxis(MouseHorizontalAxis), UnityEngine.Input.GetAxis(MouseVerticalAxis));

            return UnityEngine.Input.GetMouseButton(RightMouseButton);
        }

        public bool CloseInventory()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.E);
        }

        public bool OpenInventory()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.E);
        }

        public bool PressedAttack()
        {
            return UnityEngine.Input.GetMouseButton(LeftMouseButton) && !MouseOverUI;
        }

        public bool Attack()
        {
            return UnityEngine.Input.GetMouseButtonDown(LeftMouseButton) && !MouseOverUI;
        }

        public bool Jump()
        {
            return UnityEngine.Input.GetKey(KeyCode.Space);
        }

        public bool Zoom()
        {
            return UnityEngine.Input.GetMouseButton(RightMouseButton) && !MouseOverUI;
        }

        public bool Sprint()
        {
            return true;
        }

        public bool DropItem()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Q);
        }

        public bool InventoryItemSelected(out int index)
        {
            index = -1;

            foreach (var key in _inventorySlotKeyCodes.Keys.Where(UnityEngine.Input.GetKeyDown))
                index = _inventorySlotKeyCodes[key];

            return index > -1;
        }

        public float ChangeCameraDistance()
        {
            if (MouseOverUI)
                return 0.0f;

            var scroll = UnityEngine.Input.GetAxis(MouseScrollWheel);

            return scroll switch
            {
                > 0f => -_stepChangeDistanceCamera,
                < 0f => _stepChangeDistanceCamera,
                _ => 0
            };
        }

        public bool RemoveBlock()
        {
            return UnityEngine.Input.GetMouseButton(LeftMouseButton) && !MouseOverUI;
        }

        public bool UseItem()
        {
            return UnityEngine.Input.GetMouseButtonDown(LeftMouseButton) && !MouseOverUI;
        }

        public Vector2 FirstPersonInput(bool _)
        {
            return new Vector2(Screen.width / 2f, Screen.height / 2f);
        }

        public Vector2 ThirdPersonInput(bool _)
        {
            return UnityEngine.Input.mousePosition;
        }
    }
}
