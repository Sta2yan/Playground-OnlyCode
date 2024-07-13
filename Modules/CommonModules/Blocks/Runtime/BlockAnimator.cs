using System.Collections;
using UnityEngine;

namespace Agava.Blocks
{
    public class BlockAnimator : MonoBehaviour, IBlockAnimation
    {
        private const string Attack = "Hit";
        private const string Idle = "Idle";
        private const int HandsLayerIndex = 1;
        
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _root;
        [SerializeField, Min(0f)] private float _speedTransitionDuration = 0.1f;
        [SerializeField, Min(0f)] private float _hitDuration;

        private Coroutine _hittingCoroutine;
        
        public void Place()
        {
            if (_root.activeSelf == false)
                return;

            if (_hittingCoroutine != null)
                StopCoroutine(_hittingCoroutine);
            
            _hittingCoroutine = StartCoroutine(Placing());
        }
        
        private IEnumerator Placing()
        {
            _animator.Play("Empty", HandsLayerIndex);
            
            _animator.SetLayerWeight(HandsLayerIndex, 1f);
            _animator.SetTrigger(Attack);

            if (_handAnimator != null)
            {
                _handAnimator.Play(Idle);
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
