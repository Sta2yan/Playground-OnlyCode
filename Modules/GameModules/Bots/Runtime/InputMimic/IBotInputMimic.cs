using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public interface IBotInputMimic
    {
        bool TryAttack();
        bool TryPlaceBlock(Vector3 position);
        void RemoveBlock(Vector3 position);
        bool TryMove(float horizontal, float vertical);
        bool TryEnableSprint(float horizontal, float vertical);
        void DisableSprint();
        void Stop();
        bool TryJump();
        void LookAt(Transform target);
    }
}
