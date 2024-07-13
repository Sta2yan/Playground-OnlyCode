using Agava.AdditionalPredefinedMethods;
using Agava.ExperienceSystem;
using UnityEngine;

namespace Agava.Playground3D.ExperienceSystemConnection
{
    public class ExperienceSystemRouter : IGameLoop
    {
        private readonly LevelExperiencePlayer _levelExperiencePlayer;
        private readonly ExperiencePlayer _experiencePlayer;
        private readonly ExperienceEventsContainer _experienceEventContainer;
        private readonly GainingExperienceView _gainingExperienceView;
        private readonly LevelGate _levelGate;

        private int _playerLevel;

        public ExperienceSystemRouter(ExperiencePlayer experiencePlayer, LevelExperiencePlayer levelExperiencePlayer,
            ExperienceEventsContainer experienceEventContainer, GainingExperienceView gainingExperienceView,
            LevelGate levelGate)
        {
            _levelExperiencePlayer = levelExperiencePlayer;
            _experiencePlayer = experiencePlayer;
            _experienceEventContainer = experienceEventContainer;
            _gainingExperienceView = gainingExperienceView;
            _levelGate = levelGate;

            _playerLevel = levelExperiencePlayer.Value;
        }

        public void Update(float deltaTime)
        {
            if (_experienceEventContainer.TryExecuteEvent(out ExperienceEvent experienceEvent))
                if (experienceEvent.Experience != 0)
                    _gainingExperienceView.Execute(experienceEvent.Experience, () => ExecuteEvent(experienceEvent));

            UpdateContentLock();
        }

        private void ExecuteEvent(ExperienceEvent experienceEvent)
        {
#if UNITY_EDITOR
            Debug.Log($"Добавлено очков опыта: {experienceEvent.Experience}");
#endif

            _experiencePlayer.AddScore(experienceEvent.Experience);
            UpdateContentLock();
        }

        private void UpdateContentLock()
        {
            if (_levelGate == null)
                return;

            if (_levelGate.NeedUpdate || (_levelExperiencePlayer.Value > _playerLevel))
            {
                _levelGate?.UnlockContent(_levelExperiencePlayer);
                _playerLevel = _levelExperiencePlayer.Value;
            }
        }
    }
}
