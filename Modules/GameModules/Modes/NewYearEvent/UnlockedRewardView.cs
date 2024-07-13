using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.NewYearEvent
{
    public class UnlockedRewardView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _description;

        public void Render(UnlockedReward reward)
        {
            _icon.sprite = reward.Icon;
            _description.text = LeanLocalization.GetTranslationText(reward.Name, reward.Name);
        }
    }
}
