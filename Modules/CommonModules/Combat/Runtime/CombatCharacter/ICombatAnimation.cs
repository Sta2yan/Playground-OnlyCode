namespace Agava.Combat
{
    public interface ICombatAnimation
    {
        void Hit();
        void Shoot();
        void EnableZoom();
        void EnableZoomTwoHand();
        void DisableZoom();
    }
}