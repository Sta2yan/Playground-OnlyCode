using System;
using Agava.Save;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class ExperienceChest : MonoBehaviour
    {
        private const string SaveKey = "ExperienceChestOpenSave";
        
        [SerializeField, Min(0)] private int _experienceCount;
        [SerializeField, Tooltip("IExperienceChestView")] private MonoBehaviour _view;
        [SerializeField] private Transform _target;
        [SerializeField, Min(0)] private float _distanceToActive;

        public bool Activate { get; private set; }
        public bool CanActivate { get; private set; }
        public ExperienceEvent ExperienceEvent { get; private set; }
        public string SaveKeyName => SaveKey + transform.position;
        public bool Collect
        {
            get => SaveFacade.GetInt(SaveKey + transform.position) == 1;
            private set => SaveFacade.SetInt(SaveKey + transform.position, value ? 1 : 0);
        }

        private IExperienceChestView View => (IExperienceChestView) _view;

        private void OnValidate()
        {
            if (_view == null) return;
            if (_view is IExperienceChestView) return;
            
            _view = null;
            throw new InvalidOperationException(nameof(_view) + " is not IExperienceChestView");
        }

        private void Awake()
        {
            ExperienceEvent = new ExperienceEvent(_experienceCount);
            
            if (Collect)
                View?.Execute();
        }

        private void Update()
        {
            CanActivate = Vector3.Distance(transform.position, _target.position) <= _distanceToActive;
        }

        /*private void OnMouseDown()
        {
            if (CanActivate == false)
                return;
            
            if (Collect)
                return;
            
            Activate = true;
            Collect = true;

            View?.Execute();
        }*/

        public void TryOpen()
        {
            if (CanActivate == false)
                return;
            
            if (Collect)
                return;
            
            Activate = true;
            Collect = true;

            View?.Execute();
        }

        public ExperienceEvent Execute()
        {
            Activate = false;
            return ExperienceEvent;
        }

        public void Deactivate()
        {
            Collect = true;
            View?.Execute();
        } 
        
        public void Restore()
        {
            Activate = false;
            Collect = false;
            
            View?.Restore();
        }
    }
}
