using UnityEngine;

namespace Agava.Combat.Weapon.Sample
{
    internal class GunTest : MonoBehaviour
    {
        [SerializeField] private Gun _gun;

        private void Awake()
        {
            _gun.Initialize(new TestMagazine(), null, false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _gun.Shot(true);
        }
    }
}
