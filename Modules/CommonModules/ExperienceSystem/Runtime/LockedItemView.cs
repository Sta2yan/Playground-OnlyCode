using TMPro;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class LockedItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _level;

        public void Render(ILevelGateContent content)
        {
            _level.text = content.UnlockingLevel.ToString();
        }
    }
}
