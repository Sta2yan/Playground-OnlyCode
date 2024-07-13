using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Combat
{
    public class GunAnimator : MonoBehaviour
    {
        private const string Shot = "Shot";

        [SerializeField] private Animator _animator;

        internal void Shoot()
        {
            _animator.SetTrigger(Shot);
        }
    }
}
