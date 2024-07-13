using UnityEngine;

namespace Agava.Combat
{
    internal class DamageView : MonoBehaviour
    {
        public void Render(int damage)
        {
            Debug.Log($"Took {damage} damage");
        }
    }
}