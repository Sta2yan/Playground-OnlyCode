using System;
using Agava.Save;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class ChestRestoreTimer : MonoBehaviour
    {
        [SerializeField] private ExperienceChest _experienceChest;
        [SerializeField] private float _hoursToReward;
        [SerializeField] private bool _availableImmediately;
        
        public DateTime TimeToReward => LastRewardTime.AddHours(_hoursToReward);
        
        public bool Activate
        {
            get => SaveFacade.GetInt(ActivateSaveKey) == 1;
            private set => SaveFacade.SetInt(ActivateSaveKey, value ? 1 : 0);
        }
            
        private string ActivateSaveKey => _experienceChest.SaveKeyName + "Activate";
        private string TimerSaveKey => _experienceChest.SaveKeyName + "Timer";
        private string FirstEnterSaveKey => _experienceChest.SaveKeyName + "FirstEnter";
        private bool NeedActivate => _experienceChest.Collect && Activate == false;

        private DateTime LastRewardTime
        {
            get => !string.IsNullOrEmpty(SaveFacade.GetString(TimerSaveKey)) ? DateTime.Parse(SaveFacade.GetString(TimerSaveKey)) : throw new NullReferenceException(nameof(LastRewardTime));
            set => SaveFacade.SetString(TimerSaveKey, value.ToString());
        }

        private bool FirstEnter
        {
            get => SaveFacade.GetInt(FirstEnterSaveKey) == 1;
            set => SaveFacade.SetInt(FirstEnterSaveKey, value ? 1 : 0);
        }

        private void Awake()
        {
            if (_availableImmediately) 
                return;
            
            if (FirstEnter)
                return;
            
            _experienceChest.Deactivate();
            Execute();
            FirstEnter = true;
        }

        private void Update()
        {
            if (NeedActivate)
            {
                Execute();
            }

            if (Activate)
            {
                if (DateTime.Now >= LastRewardTime.AddHours(_hoursToReward))
                {
                    Activate = false;
                    _experienceChest.Restore();
                }
            }
        }

        private void Execute()
        {
            LastRewardTime = DateTime.Now;
            Activate = true;
        }
    }
}
