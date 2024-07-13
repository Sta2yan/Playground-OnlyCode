using Agava.Combat;
using UnityEngine;

namespace Agava.Playground3D.Dynamite
{
    public class DynamiteBulletTarget : BulletTarget
    {
        [SerializeField] private Dynamite _dynamite;

        public override void Hit()
        {
            _dynamite.Activate(0.0f);
        }
    }
}
