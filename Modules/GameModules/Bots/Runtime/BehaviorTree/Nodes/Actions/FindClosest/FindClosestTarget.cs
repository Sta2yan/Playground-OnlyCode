using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    internal class FindClosestTarget<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
    {
        public SharedFloat Radius;
        public TSharedObject ClosestObject;
        public SharedLayerMask LayerMask;

        private readonly Collider[] _overlapColliders = new Collider[16];

        public override TaskStatus OnUpdate()
        {
            int collidersCount = Physics.OverlapSphereNonAlloc(transform.position, Radius.Value, _overlapColliders, LayerMask.Value.value);
            TObject closestObject = null;
            float closestObjectDistance = float.PositiveInfinity;

            Collider collider;

            for (int i = 0; i < collidersCount; i++)
            {
                collider = _overlapColliders[i];

                if (collider.TryGetComponent(out TObject detectedObject))
                {
                    if (IgnoreFoundObject(detectedObject))
                        continue;

                    float distance = Vector3.Distance(transform.position, detectedObject.transform.position);

                    if (distance < closestObjectDistance)
                    {
                        closestObject = detectedObject;
                        closestObjectDistance = distance;
                    }
                }
            }

            ClosestObject.Value = closestObject;
            return TaskStatus.Success;
        }

        protected virtual bool IgnoreFoundObject(TObject foundObject)
        {
            return false;
        }
    }
}
