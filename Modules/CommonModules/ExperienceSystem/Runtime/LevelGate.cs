using System.Collections.Generic;

namespace Agava.ExperienceSystem
{
    public class LevelGate
    {
        private readonly bool _allContentUnlocked = false;
        private readonly LockedItemsList _lockedItemsList;

        private List<ILevelGateContentContainer> _levelGateContentContainers = new();

        public bool NeedUpdate { get; private set; } = true;

        public LevelGate(LockedItemsList lockedItemsList, bool allContentUnlocked)
        {
#if UNITY_EDITOR
            _allContentUnlocked = allContentUnlocked;
#endif

            _lockedItemsList = lockedItemsList;
            _allContentUnlocked = allContentUnlocked;
        }

        public void AddContainer(ILevelGateContentContainer container)
        {
            if (_levelGateContentContainers.Contains(container) == false)
            {
                _levelGateContentContainers.Add(container);
                NeedUpdate = true;
            }
        }

        public void UnlockContent(LevelExperiencePlayer levelExperiencePlayer)
        {
            foreach (ILevelGateContentContainer container in _levelGateContentContainers)
                container.TryUnlockContent(_lockedItemsList, levelExperiencePlayer.Value, out List<ILevelGateContent> unlockedContent, _allContentUnlocked);
        }
    }
}
