using Agava.AdditionalPredefinedMethods;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class StageRewardRouter : IGameLoop
    {
        private readonly CollectingCharacter _targetCharacter;
        private readonly FinishPanel _finishPanel;
        private readonly IStageReward _stageReward;

        private bool _maxProgressReached = false;

        public StageRewardRouter(CollectingCharacter collectingCharacter, FinishPanel finishPanel, IStageReward stageReward)
        {
            _targetCharacter = collectingCharacter;
            _finishPanel = finishPanel;
            _stageReward = stageReward;
        }

        public void Update(float _)
        {
            if (_maxProgressReached)
                return;

            if (_targetCharacter == null || _stageReward == null)
                return;

            if (_targetCharacter.MaxProgressReached)
            {
                _maxProgressReached = true;

                UnityEngine.Time.timeScale = 0;

#if !ANDROID_BUILD || UNITY_EDITOR
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
#endif

                if (_stageReward.TryActivate())
                {
                    _stageReward.ShowView(() => _finishPanel.Show());
                }
                else
                {
                    _finishPanel.Show();
                }
            }
        }
    }
}
