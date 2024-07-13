using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Ragdoll
{
    public class JointSetter : MonoBehaviour
    {
        [SerializeField] private Joint _joint;
        [SerializeField] private Rigidbody _rigidbody;

        public void SetConnectedBod(Rigidbody rigidbody)
        {
            _joint.connectedBody = rigidbody;
        }

        public void Unonnect()
        {
            _joint.connectedBody = null;
        }
    }
}
