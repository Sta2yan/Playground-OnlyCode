using Agava.Movement;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class MainMenuBotInputMimic : IBotInputMimic
    {
        private readonly CharacterMovementAdapter _movement;

        public MainMenuBotInputMimic(CharacterMovementAdapter movement)
        {
            _movement = movement;
        }

        public void LookAt(Transform target)
        {
            _movement.LookAt(target);
        }

        public bool TryMove(float horizontal, float vertical)
        {
            return _movement.TryMove(horizontal, vertical);
        }

        public void Stop()
        {
            _movement.TryMove(0, 0);
            _movement.DisableSprint();
        }

        public bool TryEnableSprint(float horizontal, float vertical)
        {
            return _movement.TryEnableSprint(horizontal, vertical);
        }

        public void DisableSprint()
        {
            _movement.DisableSprint();
        }

        public bool TryJump()
        {
            return _movement.TryJump();
        }

        public bool TryAttack() => false;

        public bool TryPlaceBlock(Vector3 position) => false;

        public void RemoveBlock(Vector3 position) { }
    }
}
