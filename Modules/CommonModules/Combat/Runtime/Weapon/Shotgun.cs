using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agava.Combat
{
    public class Shotgun : Gun
    {
        [SerializeField, Min(1)] private int _bulletPerShoot;
        [SerializeField] private Vector2 _spread;

        protected override void AdditionalShotRules(Action onKill = null, Action onHit = null)
        {
            if (CanShot == false)
                throw new InvalidOperationException();

            for (int i = 0; i < _bulletPerShoot; i++)
                DefaultShot(onKill, onHit);
        }

        protected override Vector3 AdditionalDirectionRules()
        {
            var shootDirection = base.AdditionalDirectionRules();

            shootDirection += new Vector3(Random.Range(_spread.x, _spread.y), Random.Range(_spread.x, _spread.y), 0);

            return shootDirection;
        }
    }
}