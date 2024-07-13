using System;
using UnityEngine;

namespace Agava.Combat
{
    public class TrapTrigger : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _damage;
        [SerializeField, Min(0)] private float _delay;

        private float _currentTime;

        private void Awake()
        {
            _currentTime = _delay;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out TrapTarget target))
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _delay)
                {
                    target.Execute(_damage);
                    _currentTime = 0;
                }
            }
        }

        protected void OnAdditionalTrigger<T>(Collider collider, Action<T> onTriggerStay) where T : Component
        {
            if (collider.TryGetComponent(out T component) == false)
                component = collider.GetComponentInChildren<T>();

            if (component == null)
                return;

            onTriggerStay?.Invoke(component);
            _currentTime = 0;
        }
    }
}
