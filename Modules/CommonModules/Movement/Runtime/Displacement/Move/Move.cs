using UnityEngine;

namespace Agava.Movement
{
    public abstract class Move : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField, Min(0f)] private float _rotationSpeed = 8f;
        [SerializeField, Min(0f)] private float _stoppingSpeed;
        [SerializeField, Min(0f)] private float _speed = 5f;
        [SerializeField] private Transform _model;

        private float _speedMultiplier;

        internal bool CanMove { get; private set; } = true;
        internal bool Moving { get; private set; }

        private void Update()
        {
            CorrectSpeedMultiplier();
        }

        public bool TryMove(float horizontal, float vertical, bool autoRotate = false)
        {
            if (CanMove == false)
            {
                _speedMultiplier = 0;
                return false;
            }

            GetDirection(horizontal, vertical, out Vector3 rawDirection, out Vector3 direction);

            Moving = rawDirection.magnitude > float.Epsilon;
            _rigidbody.velocity = direction * (_speedMultiplier * _speed) + Vector3.up * _rigidbody.velocity.y;

            if (Moving || autoRotate)
                _model.rotation = Quaternion.Lerp(_model.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

            return true;
        }

        public void Enable()
        {
            CanMove = true;
        }

        public void LookAt(Transform target)
        {
            Vector3 direction = target.position - _model.position;
            direction.y = 0;
            _model.rotation = Quaternion.Lerp(_model.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);
        }

        internal void ChangeSpeedMultiplier(float value)
        {
            _speedMultiplier = value;
        }

        internal void ResetSpeedMultiplier()
        {
            _speedMultiplier = 0f;
        }

        protected abstract void GetDirection(float horizontal, float vertical, out Vector3 rawDirection, out Vector3 direction);

        private void CorrectSpeedMultiplier()
        {
            if (Moving)
                return;

            if (_stoppingSpeed > 0)
                _speedMultiplier = Mathf.MoveTowards(_speedMultiplier, 0f,
                    (_stoppingSpeed + Mathf.Lerp(0f, .5f, _speedMultiplier)) * Time.deltaTime);
            else
                _speedMultiplier = 0;
        }
    }
}
