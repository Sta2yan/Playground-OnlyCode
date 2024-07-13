using UnityEngine;

namespace Agava.Movement
{
    public class BotMove : Move
    {
        protected override void GetDirection(float horizontal, float vertical, out Vector3 rawDirection, out Vector3 direction)
        {
            rawDirection = new Vector3(horizontal, 0, vertical).normalized;

            if (rawDirection != Vector3.zero)
                direction = Quaternion.Euler(0f, 0f, 0f) * rawDirection;
            else
                direction = new Vector3(transform.forward.x, 0f, transform.forward.z);
        }
    }
}
