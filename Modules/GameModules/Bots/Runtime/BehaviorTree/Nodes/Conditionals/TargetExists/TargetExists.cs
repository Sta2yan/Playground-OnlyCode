using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    internal class TargetExists<TObject, TSharedObject> : Conditional where TSharedObject : SharedVariable<TObject>
    {
        public TSharedObject Target;

        public override TaskStatus OnUpdate()
        {
            return Target.Value == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
