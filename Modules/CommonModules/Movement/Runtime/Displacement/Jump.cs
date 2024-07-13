using System;
using UnityEngine;

namespace Agava.Movement
{
    public class Jump : MonoBehaviour
    {
        private const float TimeToEnableMoveAfterJump = .03f;
        private const float TimeToEnableJump = .1f;
        private const float TimeToEndJump = .1f;
        private const int StepToExecute = 5;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Move _move;
        [SerializeField, Min(0f)] private float _groundCollisionDistance = 1.03f;
        [SerializeField, Min(0f)] private float _jumpHeight = 2f;
        [SerializeField, Min(0f)] private Vector3 _groundCollisionSize = new(.5f, .5f, .5f);
        [SerializeField] private LayerMask _groundCollisions;

        private bool _canJump = true;
        private bool _jumping;

        private int _currentTimeStep;

        public bool Grounded { get; private set; } = true;
        public Collider LastHitCollider { get; private set; }

        private void FixedUpdate()
        {
            _currentTimeStep += 1;

            if (_currentTimeStep < StepToExecute)
                return;

            UpdateGroundCollisions();
            FallControl();

            _currentTimeStep = 0;
        }

        public bool TryJump()
        {
            if (Grounded == false)
                return false;

            if (_canJump == false)
                return false;

            _move.ResetSpeedMultiplier();

            _jumping = true;
            _canJump = false;

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(new Vector3(0f, Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), 0f), ForceMode.VelocityChange); ;

            if (IsInvoking(nameof(EnableJump)) == false)
                Invoke(nameof(EnableJump), TimeToEnableJump);

            return true;
        }

        private void EnableJump()
        {
            _canJump = true;
        }

        private void EndJump()
        {
            _jumping = false;

            if (IsInvoking(nameof(EnableMove)) == false)
                Invoke(nameof(EnableMove), TimeToEnableMoveAfterJump);
        }

        private void FallControl()
        {
            if (Grounded && _jumping == false)
            {
                _move.Enable();
                return;
            }

            _move.Enable();

            if (IsInvoking(nameof(EndJump)) == false)
                Invoke(nameof(EndJump), TimeToEndJump);
        }

        private void EnableMove()
        {
            _move.Enable();
        }

        private void UpdateGroundCollisions()
        {
            Grounded = Physics.BoxCast(transform.position, _groundCollisionSize / 2f, Vector3.down, out RaycastHit hitInfo, Quaternion.identity, _groundCollisionDistance, _groundCollisions);
            LastHitCollider = hitInfo.collider;
        }

        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(transform.position, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(transform.position + Vector3.left * _groundCollisionSize.x / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(transform.position + Vector3.right * _groundCollisionSize.x / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(transform.position + Vector3.forward * _groundCollisionSize.z / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawRay(transform.position + Vector3.back * _groundCollisionSize.z / 2f, Vector3.down * _groundCollisionDistance, Color.yellow, 0f, false);
            Debug.DrawLine(transform.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), transform.position + new Vector3(_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), Color.yellow, 0f, false);
            Debug.DrawLine(transform.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), transform.position + new Vector3(_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), Color.yellow, 0f, false);
            Debug.DrawLine(transform.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), transform.position + new Vector3(-_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), Color.yellow, 0f, false);
            Debug.DrawLine(transform.position + new Vector3(_groundCollisionSize.x / 2f, 0f, -_groundCollisionSize.z / 2f), transform.position + new Vector3(_groundCollisionSize.x / 2f, 0f, _groundCollisionSize.z / 2f), Color.yellow, 0f, false);
        }
    }
}
