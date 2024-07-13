using System;

namespace Agava.Playground3D.NewYearEvent
{
    public interface IStageReward
    {
        bool TryActivate();
        void ShowView(Action onViewHide);
    }
}
