namespace Agava.Movement
{
    public interface ICharacterMovementAnimation
    {
        void ChangeSpeed(float normalizedSpeed);
        void ChangeGrounded(bool value);
    }
}