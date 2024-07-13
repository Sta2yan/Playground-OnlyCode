using Agava.Playground3D.Items;

namespace Agava.ExperienceSystem
{
    public interface IItemExperienceEventRule
    {
        public bool TryGetExperienceEvent(IItem item, out ExperienceEvent experienceEvent);
    }
}
