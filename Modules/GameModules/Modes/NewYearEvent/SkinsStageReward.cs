using UnityEngine;
using Agava.Save;
using System;

namespace Agava.Playground3D.NewYearEvent
{
    public class SkinsStageReward : MonoBehaviour, IStageReward
    {
        [SerializeField] private UnlockedReward[] _rewards;
        [SerializeField] private SkinsStageRewardView _view;

        private UnlockedReward _unlockedReward;

        public bool TryActivate()
        {
            string saveKey;

            foreach (UnlockedReward reward in _rewards)
            {
                saveKey = reward.SaveKey;

                if (string.IsNullOrEmpty(saveKey) || SaveFacade.HasKey(saveKey))
                    continue;

                _unlockedReward = reward;
                SaveFacade.SetInt(saveKey, 1);
                return true;
            }

            return false;
        }

        public void ShowView(Action onViewHide)
        {
            _view.Show(onViewHide, _unlockedReward);
        }
    }
}
