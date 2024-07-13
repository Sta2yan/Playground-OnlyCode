using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Combat
{
    public class RedBlood : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public bool IsRedBlood { get; private set; } = true;

        private void OnEnable()
        {
            _button.onClick.AddListener(SwitchState);
        }

        private void OnDisable()
        {
            _button.onClick.AddListener(SwitchState);
        }

        private void SwitchState()
        {
            IsRedBlood = !IsRedBlood;
        }
    }
}
