using UnityEngine;

namespace Agava.Combat
{
    public interface ICombatCharacter
    {
        bool Alive { get; }
        Vector3 Forward { get; }

        void Hit(int damage, Vector3 pushForce);
        void Move(Vector3 position);
        void Revive();
        void Destroy();
    }
}
