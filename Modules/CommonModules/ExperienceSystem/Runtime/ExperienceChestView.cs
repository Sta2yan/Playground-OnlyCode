using System.Collections.Generic;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class ExperienceChestView : MonoBehaviour, IExperienceChestView
    {
        private const string OpenTrigger = "Open";
        private const string CloseTrigger = "Close";

        [SerializeField] private ExperienceChest _experienceChest;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private List<ParticleSystem> _particles;

        private void Update()
        {
            _canvas.SetActive(!_experienceChest.Collect && _experienceChest.CanActivate);
        }

        public void Execute()
        {
            _animator.SetTrigger(OpenTrigger);

            foreach (var particle in _particles)
                particle.Play();
        }

        public void Restore()
        {
            _animator.SetTrigger(CloseTrigger);

            foreach (var particle in _particles)
                particle.Stop();
        }
    }
}
