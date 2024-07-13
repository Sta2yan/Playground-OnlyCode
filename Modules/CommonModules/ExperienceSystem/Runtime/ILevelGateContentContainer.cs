using System.Collections.Generic;

namespace Agava.ExperienceSystem
{
    public interface ILevelGateContentContainer
    {
        bool TryUnlockContent(LockedItemsList lockedItemsList, int playerLevel, out List<ILevelGateContent> unlockedContent, bool instaUnlock = false);
    }
}
