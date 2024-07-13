namespace Agava.Movement
{
    public interface IDisplacementObject
    {
        bool TryMove(float horizontal, float vertical, bool autoRotate = false);
        bool TryEnableSprint(float horizontal, float vertical);
        bool TryJump();
        void DisableSprint();
    }
}
