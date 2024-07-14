using UnityEngine;

namespace Agava.Utils
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void Update()
        {
            transform.position = _target.position;
        }
    }
}
