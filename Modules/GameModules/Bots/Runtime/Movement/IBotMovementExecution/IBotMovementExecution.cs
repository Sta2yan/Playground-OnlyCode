using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public interface IBotMovementExecution
    {
        void ExecuteMovement(Vector3 direction);
        void Stop();
    }
}
