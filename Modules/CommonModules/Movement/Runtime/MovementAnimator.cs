using System;
using System.Collections;
using UnityEngine;

namespace Agava.Movement
{
    public class MovementAnimator : MonoBehaviour, ICharacterMovementAnimation
    {
        private const string Grounded = "Grounded";
        private const string Speed = "Speed";

        [SerializeField] private Animator _animator;
        [SerializeField] private Sprint _sprint;
        [SerializeField] private Move _move;
        [SerializeField] private Jump _jump;
        [SerializeField, Min(0f)] private float _speedTransitionDuration = 0.1f;
        
        private Coroutine _smoothChangingSpeedCoroutine;
        private float _lastTargetSpeed;

        private void Update()
        {
            if (_sprint == null || _move == null || _jump == null)
                return;
            
            ChangeSpeed(_move.Moving ? _sprint.Active ? 1f : 0.5f : 0f);
            ChangeGrounded(_jump.Grounded);
        }

        public void ChangeSpeed(float normalizedSpeed)
        {
            if (Math.Abs(normalizedSpeed - _lastTargetSpeed) < 0.01f)
                return;
            
            if (_smoothChangingSpeedCoroutine != null)
                StopCoroutine(_smoothChangingSpeedCoroutine);

            _smoothChangingSpeedCoroutine = StartCoroutine(SmoothChangingSpeed(normalizedSpeed, _speedTransitionDuration));
        }
        
        public void ChangeGrounded(bool value)
        {
            _animator.SetBool(Grounded, value);
        }
        
        private IEnumerator SmoothChangingSpeed(float targetValue, float duration)
        {
            float currentTargetValue = _lastTargetSpeed;
            _lastTargetSpeed = targetValue;
            
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _animator.SetFloat(Speed, Mathf.Lerp(currentTargetValue, targetValue, elapsedTime / duration));

                yield return null;
            }
            
            _animator.SetFloat(Speed, targetValue);
        }
    }
}
