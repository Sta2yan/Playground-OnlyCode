using UnityEngine;

namespace Agava.Combat
{
    public class MachineGun : Gun
    {
        [SerializeField] private Vector2 _spread;
        
        protected override Vector3 AdditionalDirectionRules()
        {
            var shootDirection = base.AdditionalDirectionRules();

            shootDirection += new Vector3(Random.Range(_spread.x, _spread.y), Random.Range(_spread.x, _spread.y), 0);
            
            return shootDirection;
        }
    }
}