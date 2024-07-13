using UnityEngine;

namespace Agava.Movement
{
    public class CharacterMovementAdapter : IDisplacementObject
    {
        private readonly Sprint _sprint;
        private readonly Move _move;
        private readonly Jump _jump;

        public CharacterMovementAdapter(Move move, Jump jump, Sprint sprint)
        {
            _sprint = sprint;
            _move = move;
            _jump = jump;
        }

        public bool TryMove(float horizontal, float vertical, bool autoRotate = false)
        {
            return _move.TryMove(horizontal, vertical, autoRotate);
        }

        public bool TryEnableSprint(float horizontal, float vertical)
        {
            return _sprint.TryEnableSprint(horizontal, vertical);
        }

        public bool TryJump()
        {
            return _jump.TryJump();
        }

        public void DisableSprint()
        {
            _sprint.DisableSprint();
        }

        public void LookAt(Transform target)
        {
            _move.LookAt(target);
        }
    }
}
