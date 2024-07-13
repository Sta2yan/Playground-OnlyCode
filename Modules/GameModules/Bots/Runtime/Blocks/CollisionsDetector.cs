using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class CollisionsDetector : MonoBehaviour
    {
        private const float MaxJumpHeight = 2f;
        private const float CollisionDistance = 1.03f;

        private readonly Vector3 _collisionSize = new Vector3(.3f, .5f, .3f);

        [SerializeField] private Transform _jumpPoint;
        [SerializeField] private LayerMask _collisionsMask;
        [SerializeField] private Transform _model;

        public Vector3 GroundBlockCenter => _jumpPoint.position + Vector3.down / 2f;
        public bool GroundExists => Physics.BoxCast(transform.position, _collisionSize / 2, Vector3.down, Quaternion.identity, CollisionDistance, _collisionsMask);
        public bool PathBlocked => Physics.BoxCast(transform.position, _collisionSize / 2f, _model.forward, Quaternion.identity, CollisionDistance, _collisionsMask);
    }
}
