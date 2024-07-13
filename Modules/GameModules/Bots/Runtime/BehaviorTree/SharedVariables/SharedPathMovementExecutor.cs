using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    [System.Serializable]
    internal class SharedPathMovement : SharedVariable<PathMovement>
    {
        public static implicit operator SharedPathMovement(PathMovement value) { return new SharedPathMovement { Value = value }; }
    }
}
