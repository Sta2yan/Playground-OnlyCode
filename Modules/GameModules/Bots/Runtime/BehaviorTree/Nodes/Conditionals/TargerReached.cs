using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Agava.Playground3D.Bots
{
    public class TargerReached : Conditional
    {
        public SharedBool TargetReached;

        public override TaskStatus OnUpdate()
        {
            return TargetReached.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
