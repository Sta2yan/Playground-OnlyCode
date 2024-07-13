using Lean.Localization;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    [CreateAssetMenu(fileName = "UnlockedReward", menuName = "Create UnlockedReward", order = 51)]
    public class UnlockedReward : ScriptableObject
    {
        [field: SerializeField] public string SaveKey { get; private set; }
        [field: SerializeField, LeanTranslationName] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
