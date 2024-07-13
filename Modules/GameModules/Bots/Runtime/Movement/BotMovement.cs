using UnityEngine;
using Agava.Movement;

namespace Agava.Playground3D.Bots
{
    internal class BotMovement : MonoBehaviour, IBotMovement
    {
        private const float TimeToEnableMoveAfterJump = .03f;
        private const float TimeToEnableJump = .1f;
        private const float TimeToEndJump = .1f;
        private const string TransformName = "DirectionPoint";

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _model;
        [SerializeField, Min(0f)] private float _groundCollisionDistance = 1.03f;
        [SerializeField, Min(0f)] private float _wallCollisionDistance = 1.03f;
        [SerializeField, Min(0f)] private float _sprintSpeedMultiplier = 1.5f;
        [SerializeField, Min(0f)] private float _rotationSpeed = 15f;
        [SerializeField, Min(0f)] private float _stoppingSpeed;
        [SerializeField, Min(0f)] private float _jumpHeight = 2f;
        [SerializeField, Min(0f)] private float _speed = 5f;
        [SerializeField, Min(0f)] private Vector3 _groundCollisionSize = new Vector3(.3f, .5f, .3f);
        [SerializeField, Min(0f)] private Vector3 _wallCollisionSize = new Vector3(.3f, .5f, .3f);
        [SerializeField] private LayerMask _collisions;

        private ICharacterMovementAnimation _characterMovementAnimation;
        private Transform _direction;
        private float _speedMultiplier;
        private bool _grounded = true;
        private bool _canJump = true;
        private bool _canMove = true;
        private bool _running;
        private bool _jumping;
        private bool _moving;

        private void Awake()
        {
            _direction = CreateEmptyTransform(transform.position, transform.rotation, transform);
        }

        private void Update()
        {
            UpdateGroundCollisions();
            TrySprinting();
            CorrectSpeedMultiplier();
            FallControl();

            _characterMovementAnimation?.ChangeSpeed(_moving ? _running ? 1f : 0.5f : 0f);
            _characterMovementAnimation?.ChangeGrounded(_grounded);
        }

        public void Initialize(ICharacterMovementAnimation characterMovementAnimation)
        {
            _characterMovementAnimation = characterMovementAnimation;
        }

        public bool TryJump()
        {
            if (_grounded == false)
                return false;

            if (_canJump == false)
                return false;

            _speedMultiplier = 0f;
            _jumping = true;
            _canJump = false;

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(new Vector3(0f, Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), 0f), ForceMode.VelocityChange);

            if (IsInvoking(nameof(EnableJump)) == false)
                Invoke(nameof(EnableJump), TimeToEnableJump);

            return true;
        }

        public bool TryMove(float horizontal, float vertical)
        {
            _canMove &= !Physics.BoxCast(transform.position, _wallCollisionSize / 2f, _model.forward, Quaternion.identity, _wallCollisionDistance, _collisions);

            if (_canMove == false)
            {
                Stop();
                return false;
            }

            Vector3 rawDirection = new Vector3(horizontal, 0, vertical).normalized;
            Vector3 direction;

            if (rawDirection != Vector3.zero)
                direction = Quaternion.Euler(0f, 0f, 0f) * rawDirection;
            else
                direction = new Vector3(transform.forward.x, 0f, transform.forward.z);

            _moving = rawDirection.magnitude > float.Epsilon;
            _rigidbody.velocity = direction * (_speedMultiplier * _speed) + Vector3.up * _rigidbody.velocity.y;

            _model.rotation = Quaternion.Lerp(_model.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

            return true;
        }

        public void Stop()
        {
            _moving = false;
            _speedMultiplier = 0;
            _rigidbody.velocity = Vector3.up * _rigidbody.velocity.y;
            DisableSprint();
        }

        public bool TryEnableSprint(float horizontal, float vertical)
        {
            if (Mathf.Abs(horizontal) > .5f || Mathf.Abs(vertical) > .5f)
                _running = true;

            return _running;
        }

        public void DisableSprint()
        {
            _running = false;
        }

        public void LookAt(Transform target)
        {
            Vector3 direction = target.position - _model.position;
            direction.y = 0;
            _model.rotation = Quaternion.Lerp(_model.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);
        }

        private void CorrectSpeedMultiplier()
        {
            if (_moving)
                return;

            if (_stoppingSpeed > 0)
                _speedMultiplier = Mathf.MoveTowards(_speedMultiplier, 0f,
                    (_stoppingSpeed + Mathf.Lerp(0f, .5f, _speedMultiplier)) * Time.deltaTime);
            else
                _speedMultiplier = 0;

            _running = false;
        }

        private void TrySprinting()
        {
            if (_moving == false)
                return;

            if (_canMove == false)
                return;

            _speedMultiplier = _running ? _sprintSpeedMultiplier : 1f;
        }

        private void UpdateGroundCollisions()
        {
            _grounded = Physics.BoxCast(transform.position, _groundCollisionSize / 2f, Vector3.down, Quaternion.identity, _groundCollisionDistance, _collisions);
        }

        private void FallControl()
        {
            if (_grounded && _jumping == false)
            {
                _canMove = true;
                return;
            }

            _canMove = true;

            if (IsInvoking(nameof(EndJump)) == false)
                Invoke(nameof(EndJump), TimeToEndJump);
        }

        private Transform CreateEmptyTransform(Vector3 position, Quaternion rotation, Transform parent)
        {
            var newTransform = new GameObject(TransformName).transform;

            newTransform.position = position;
            newTransform.rotation = rotation;
            newTransform.parent = parent;

            newTransform.hideFlags = HideFlags.HideInHierarchy;
            newTransform.gameObject.hideFlags = HideFlags.HideAndDontSave;

            return newTransform;
        }

        private void EndJump()
        {
            _jumping = false;

            if (IsInvoking(nameof(EnableMove)) == false)
                Invoke(nameof(EnableMove), TimeToEnableMoveAfterJump);
        }

        private void EnableMove()
        {
            _canMove = true;
        }

        private void EnableJump()
        {
            _canJump = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, _wallCollisionSize + _model.forward * _wallCollisionDistance);
        }
    }
}