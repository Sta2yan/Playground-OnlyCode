using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class ExperienceEvent
    {
        public int Experience { get; private set; }

        public ExperienceEvent(int experience)
        {
            Experience = experience;
        }
    }
}
