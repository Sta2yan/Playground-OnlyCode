using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.NewYearEvent
{
    public class SkinsStageRewardView : MonoBehaviour, IStageRewardView
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private UnlockedRewardView _unlockedRewardView;
        [SerializeField] private Button _okButton;

        private Action _onHide;

        private void OnEnable()
        {
            _okButton.onClick.AddListener(OnOkButtonClicked);
        }

        private void OnDisable()
        {
            _okButton.onClick.RemoveListener(OnOkButtonClicked);
        }

        public void Show(Action onHide, UnlockedReward unlockedReward)
        {
            _onHide = onHide;
            _unlockedRewardView.Render(unlockedReward);
            _root.SetActive(true);
        }

        private void OnOkButtonClicked()
        {
            _root.SetActive(false);
            _onHide?.Invoke();
        }
    }
}
