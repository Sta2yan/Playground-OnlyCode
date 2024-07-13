using System;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    [Serializable]
    public struct LevelSetting
    {
        [field: SerializeField] public int ExperienceToGet { get; private set; }
    }
}