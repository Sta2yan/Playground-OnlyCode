using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public interface IBotMovement
    {
        bool TryMove(float horizontal, float vertical);
        bool TryJump();
        bool TryEnableSprint(float horizontal, float vertical);
        void DisableSprint();
        void Stop();
        void LookAt(Transform target);
    }
}
