using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class RemoveTarget<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
    {
        public TSharedObject TargetObject;

        public override TaskStatus OnUpdate()
        {
            TargetObject.Value = null;

            return TaskStatus.Success;
        }
    }
}
