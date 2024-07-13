using UnityEngine;

namespace Agava.Playground3D.GravyGun
{
    public class GravityGun : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;

        public float MovementSpeed => _movementSpeed;
    }
}
