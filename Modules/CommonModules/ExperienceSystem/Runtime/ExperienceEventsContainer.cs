using System.Collections.Generic;

namespace Agava.ExperienceSystem
{
    public class ExperienceEventsContainer
    {
        private Stack<ExperienceEvent> _experienceEvents = new();

        public void TriggerEvent(ExperienceEvent experienceEvent)
        {
            if (experienceEvent != null)
                _experienceEvents.Push(experienceEvent);
        }

        public bool TryExecuteEvent(out ExperienceEvent experienceEvent)
        {
            experienceEvent = null;

            if (_experienceEvents.Count == 0)
                return false;

            experienceEvent = _experienceEvents.Pop();
            return true;
        }
    }
}
