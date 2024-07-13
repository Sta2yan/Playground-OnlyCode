using System;

namespace Agava.Playground3D.NewYearEvent
{
    public interface IStageRewardView
    {
        public void Show(Action onHide, UnlockedReward unlockedReward);
    }
}
