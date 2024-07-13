namespace Agava.ExperienceSystem
{
    public interface ILevelGateContent
    {
        string LockedItemId { get; }
        bool LevelGated { get; }
        bool Unlocked { get; }
        int UnlockingLevel { get; }
        bool TryUnlock(int currentLevel, bool instaUnlock = false);
    }
}
