using System.Collections.Generic;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    [CreateAssetMenu(menuName = "Create LevelExperienceConfig", fileName = "LevelExperienceConfig", order = 56)]
    public class LevelExperienceConfig : ScriptableObject
    {
        [field: SerializeField] public List<LevelSetting> LevelSettings = new();
    }
}