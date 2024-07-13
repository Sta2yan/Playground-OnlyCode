using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class MainMenuBotMovementExecution : IBotMovementExecution
    {
        private readonly IBotInputMimic _input;
        private readonly CollisionsDetector _collisionsDetector;

        public MainMenuBotMovementExecution(IBotInputMimic botInputMimic, CollisionsDetector collisionsDetector)
        {
            _input = botInputMimic;
            _collisionsDetector = collisionsDetector;
        }

        public void ExecuteMovement(Vector3 direction)
        {
            if (_input.TryMove(direction.x, direction.z) && !_collisionsDetector.PathBlocked)
            {
                _input.TryEnableSprint(direction.x, direction.z);
            }
            else
            {
                _input.TryJump();
            }
        }

        public void Stop()
        {
            _input.Stop();
        }
    }
}
