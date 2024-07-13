using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class BedWarsBotMovementExecution : IBotMovementExecution
    {
        private readonly IBotInputMimic _input;
        private readonly CollisionsDetector _collisionsDetector;

        private bool _placedOrRemovedBlock;

        public BedWarsBotMovementExecution(IBotInputMimic botInputMimic, CollisionsDetector collisionsDetector)
        {
            _input = botInputMimic;
            _collisionsDetector = collisionsDetector;
            _placedOrRemovedBlock = false;
        }

        public void ExecuteMovement(Vector3 direction)
        {
            if (_collisionsDetector.GroundExists == false && direction.y >= 0)
            {
                _input.TryPlaceBlock(_collisionsDetector.GroundBlockCenter);
                _placedOrRemovedBlock = true;
                return;
            }

            if (_input.TryMove(direction.x, direction.z))
            {
                if (_placedOrRemovedBlock == false)
                {
                    _input.TryEnableSprint(direction.x, direction.z);
                }
                else
                {
                    _input.DisableSprint();
                }

                _placedOrRemovedBlock = false;
            }
            else
            {
                _input.TryJump();
            }
        }

        public void Stop()
        {
            _input.Stop();
            _placedOrRemovedBlock = false;
        }
    }
}
