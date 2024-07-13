using System.Collections;
using UnityEngine;

namespace Agava.Combat
{
    public class CombatAnimator : MonoBehaviour, ICombatAnimation
    {
        private const string ZoomingTwoHand = "ZoomingTwoHand";
        private const string Zooming = "Zooming";
        private const string Shooting = "Shoot";
        private const string Attack = "Hit";
        private const int HandsLayerIndex = 1;
        private const int TwoHandsLayerIndex = 2;
        
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _root;
        [SerializeField, Min(0f)] private float _speedTransitionDuration = 0.1f;
        [SerializeField, Min(0f)] private float _hitDuration;

        private Coroutine _hittingCoroutine;
        private float _currentHandsLayerWeight;
        
        public void Hit()
        {
            if (_root.activeSelf == false)
                return;

            if (_hittingCoroutine != null)
                StopCoroutine(_hittingCoroutine);

            _hittingCoroutine = StartCoroutine(Hitting());
        }

        public void Shoot()
        {
            _animator.SetTrigger(Shooting);
        }
        
        public void EnableZoom()
        {
            _animator.SetLayerWeight(HandsLayerIndex, 1f);
            _animator.SetLayerWeight(TwoHandsLayerIndex, 0f);
            _animator.SetBool(Zooming, true);
        }
        
        public void EnableZoomTwoHand()
        {
            _animator.SetLayerWeight(HandsLayerIndex, 0f);
            _animator.SetLayerWeight(TwoHandsLayerIndex, 1f);
            _animator.SetBool(ZoomingTwoHand, true);
        }

        public void DisableZoom()
        {
            _animator.SetBool(Zooming, false);
            _animator.SetBool(ZoomingTwoHand, false);
            //_animator.SetLayerWeight(HandsLayerIndex, _hittingCoroutine == null ? 0f : 1f);
            _animator.SetLayerWeight(TwoHandsLayerIndex, _hittingCoroutine == null ? 0f : 1f);
        }

        private IEnumerator Hitting()
        {
            _animator.Play("Empty", HandsLayerIndex);
            
            _animator.SetLayerWeight(HandsLayerIndex, 1f);
            _animator.SetTrigger(Attack);

            if (_handAnimator != null)
            {
                _handAnimator.Play("Idle");
                _handAnimator.SetTrigger(Attack);
            }
            
            yield return new WaitForSeconds(_hitDuration);

            float elapsedTime = 0f;

            while (elapsedTime < _speedTransitionDuration)
            {
                _animator.SetLayerWeight(HandsLayerIndex, Mathf.Lerp(1f, 0f, elapsedTime / _speedTransitionDuration));
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            _animator.SetLayerWeight(HandsLayerIndex, 0f);
            _hittingCoroutine = null;
        }
    }
}
