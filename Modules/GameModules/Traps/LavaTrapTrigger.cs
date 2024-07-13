using Agava.Combat;
using UnityEngine;

namespace Agava.Playground3D.Traps
{
    public class LavaTrapTrigger : TrapTrigger
    {
        private void OnTriggerEnter(Collider collider)
        {
            OnAdditionalTrigger<Dynamite.Dynamite>(collider, (dynamite) => dynamite.Activate(dynamite.ExplosionDelay));
        }
    }
}