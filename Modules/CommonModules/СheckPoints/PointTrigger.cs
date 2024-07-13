using Agava.ExperienceSystem;
using System;
using UnityEngine;

namespace Agava.CheckPoints
{
    public class PointTrigger : MonoBehaviour
    {
        [SerializeField] private Renderer _flag;
        [SerializeField] private Vector3 _cameraRotate;

        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _triggeredEventRule;

        public bool Complete { get; private set; } = false;
        public Vector3 CameraRotate => _cameraRotate;

        public void Initialize(ExperienceEventsContainer experienceEventsContainer, ExperienceEventRule triggeredEventRule)
        {
            _experienceEventsContainer = experienceEventsContainer;
            _triggeredEventRule = triggeredEventRule;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CheckPointTarget _) && (Complete == false))
                Execute();
        }

        private void Execute()
        {
            Complete = true;

            _experienceEventsContainer.TriggerEvent(_triggeredEventRule.ExperienceEvent());
            _flag.material.color = Color.green;
        }
    }
}
