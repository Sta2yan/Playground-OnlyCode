namespace Agava.ExperienceSystem
{
    public class LevelExperiencePlayer
    {
        private readonly LevelExperienceConfig _levelExperienceConfig;
        private readonly ExperiencePlayer _experiencePlayer;

        public LevelExperiencePlayer(LevelExperienceConfig levelExperienceConfig, ExperiencePlayer experiencePlayer)
        {
            _levelExperienceConfig = levelExperienceConfig;
            _experiencePlayer = experiencePlayer;
        }
        
        public int ExperienceToNextLevel => Value + 1 >= _levelExperienceConfig.LevelSettings.Count ? 1 : _levelExperienceConfig.LevelSettings[Value + 1].ExperienceToGet;
        public int ExperienceCurrentLevel => _levelExperienceConfig.LevelSettings[Value].ExperienceToGet;
        public int ExperienceValue => _experiencePlayer.Value;
        public int Value => CurrentLevel();
        public bool LastLevel => _levelExperienceConfig.LevelSettings.Count == Value + 1; 

        private int CurrentLevel()
        {
            for (int level = 0; level < _levelExperienceConfig.LevelSettings.Count; level++)
                if (ExperienceValue < _levelExperienceConfig.LevelSettings[level].ExperienceToGet)
                    return level - 1;

            return _levelExperienceConfig.LevelSettings.Count - 1;
        }
    }
}