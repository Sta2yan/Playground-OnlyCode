using UnityEngine;

namespace Agava.Input
{
    public class MobileInput : IInput
    {
        private const float DivisorSensitivity = 10f;

        private readonly Joystick _movementJoystick;
        private readonly MobileInteractiveArea _mobileInteractiveArea;
        private readonly ButtonTrackingArea _sprint;
        private readonly ButtonTrackingArea _attack;
        private readonly ButtonTrackingArea _jump;
        private readonly ButtonTrackingArea _zoom;
        private readonly ButtonTrackingArea _drop;
        private readonly float _stepChangeDistanceCamera;

        private bool _sprinting = true;
        private bool _zooming;

        public MobileInput(Joystick movementJoystick, MobileInteractiveArea mobileInteractiveArea, float stepChangeDistanceCamera, ButtonTrackingArea jump, ButtonTrackingArea sprint, ButtonTrackingArea attack, ButtonTrackingArea zoom, ButtonTrackingArea drop)
        {
            _movementJoystick = movementJoystick;
            _mobileInteractiveArea = mobileInteractiveArea;
            _stepChangeDistanceCamera = stepChangeDistanceCamera;
            _sprint = sprint;
            _attack = attack;
            _jump = jump;
            _zoom = zoom;
            _drop = drop;
        }

        public bool InventoryItemSelected(out int index)
        {
            index = default;
            return false;
        }

        public bool RotateDirection(out Vector2 direction)
        {
            direction = default;

            if (_mobileInteractiveArea.Active == false)
                return false;

            direction = _mobileInteractiveArea.InputVector / DivisorSensitivity;
            return true;
        }

        public bool InteractiveShop()
        {
            return false;
        }

        public bool CloseInventory()
        {
            return false;
        }

        public bool OpenInventory()
        {
            return false;
        }

        public bool RemoveBlock()
        {
            return _mobileInteractiveArea.Pressed;
        }

        public bool PlaceBlock()
        {
            if (_mobileInteractiveArea.Clicked == false)
                return false;

            _mobileInteractiveArea.DisableRaycastTarget();

            return true;
        }

        public bool DropItem()
        {
            if (_drop.Clicked == false)
                return false;

            _drop.DisableClick();

            return true;
        }

        public bool Sprint()
        {
            if (_sprint == null)
                return true;

            if (_sprint.Clicked == false)
                return _sprinting;

            _sprinting = !_sprinting;
            _sprint.DisableClick();

            return _sprinting;
        }

        public bool PressedAttack()
        {
            if (_mobileInteractiveArea.Clicked == false)
                return false;

            _mobileInteractiveArea.DisableRaycastTarget();

            return true;
            //return _attack.Pressed;
        }

        public bool Attack()
        {
            if (_mobileInteractiveArea.Clicked == false)
                return false;

            _mobileInteractiveArea.DisableRaycastTarget();

            return true;
            //if (_attack.Clicked == false)
            //    return false;

            //_attack.DisableClick();

            //return true;
        }

        public bool Jump()
        {
            return _jump.Pressed;
        }

        public bool Zoom()
        {
            if (_zoom.Clicked == false)
                return _zooming;

            _zooming = !_zooming;
            _zoom.DisableClick();

            return _zooming;
        }

        public bool UseItem()
        {
            if (_mobileInteractiveArea.Clicked == false)
                return false;

            _mobileInteractiveArea.DisableRaycastTarget();

            return true;
        }

        public float ChangeCameraDistance()
        {
            return _stepChangeDistanceCamera * _mobileInteractiveArea.Loop;
        }

        public Vector2 MoveDirection()
        {
            return _movementJoystick.InputVector;
        }

        public Vector2 FirstPersonInput(bool touch)
        {
            Vector2 position;

            if (touch)
            {
#if UNITY_EDITOR
                position = UnityEngine.Input.mousePosition;
#else
position = UnityEngine.Input.GetTouch(0).position;
#endif
            }
            else
            {
                position = new Vector2(Screen.width / 2f, Screen.height / 2f);
            }

            return position;
        }

        public Vector2 ThirdPersonInput(bool touch)
        {
            Vector2 position;

            if (touch)
            {
#if UNITY_EDITOR
                position = UnityEngine.Input.mousePosition;
#else
position = UnityEngine.Input.GetTouch(0).position;
#endif
            }
            else
            {
                position = new Vector2(Screen.width / 2f, Screen.height / 2f);
            }

            return position;
        }
    }
}
