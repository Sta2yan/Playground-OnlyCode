using UnityEngine;

namespace Agava.ExperienceSystem
{
    [CreateAssetMenu(fileName = "ExperienceEventRule", menuName = "ExperienceEventRules/Create ExperienceEventRule", order = 51)]
    public class ExperienceEventRule : ScriptableObject
    {
        [field: SerializeField, Min(0)] private int _experience;

        public ExperienceEvent ExperienceEvent()
        {
            return new ExperienceEvent(_experience);
        }
    }
}
