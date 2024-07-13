using UnityEngine;

namespace Agava.Movement
{
    public interface ICameraMovement
    {
        Camera CameraMain { get; }
        bool FirstPersonPerspective { get; }
        void ChangeRotation(float vertical, float horizontal);
        void ChangeDistance(float step);
        void ChangeHorizontalOffset(float value);
        void ChangeFieldOfView(int fieldOfView);
        void ResetOffset();
    }
}
