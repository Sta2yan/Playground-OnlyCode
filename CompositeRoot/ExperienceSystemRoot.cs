using System.Collections.Generic;
using System.Linq;
using Agava.AdditionalPredefinedMethods;
using Agava.ExperienceSystem;
using Agava.Playground3D.ExperienceSystemConnection;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class ExperienceSystemRoot : CompositeRoot
    {
        [SerializeField] private LevelExperienceConfig _levelExperienceConfig;
        [SerializeField] private GainingExperienceView _gainingExperienceView;
        [SerializeField] private ExperienceSystemView _experienceSystemView;
        [SerializeField] private List<ExperienceChest> _chests;

        private LevelGate _levelGate;

        private LockedItemsList _lockedItemsList;
        private ExperienceSystemRouter _experienceSystemRouter;
        private LevelExperiencePlayer _levelExperiencePlayer;
        private ExperienceChestRouter _experienceChestRouter;
        private ExperiencePlayer _experiencePlayer;

        private ExperienceEventsContainer _experienceEventsContainer;
        private IGameLoop _gameLoop;

        public void Initialize(LockedItemsList lockedItemsList, ExperienceEventsContainer experienceEventsContainer, LevelGate levelGate = null)
        {
            _lockedItemsList = lockedItemsList;
            _experienceEventsContainer = experienceEventsContainer;
            _levelGate = levelGate;
        }

        public override void Compose()
        {
            _experiencePlayer = new ExperiencePlayer();
            _levelExperiencePlayer = new LevelExperiencePlayer(_levelExperienceConfig, _experiencePlayer);

            _experienceSystemRouter = new ExperienceSystemRouter(_experiencePlayer, _levelExperiencePlayer, _experienceEventsContainer, _gainingExperienceView, _levelGate);
            _experienceChestRouter = new ExperienceChestRouter(_chests, _experienceEventsContainer);
            _gameLoop = new GameLoopGroup(new[] { _experienceSystemRouter as IGameLoop, _experienceChestRouter });

            _experienceSystemView.Initialize(_lockedItemsList, _levelExperiencePlayer, _experiencePlayer);
        }

        public void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }

#if UNITY_EDITOR
        [ProButton]
        public void FindAllChests()
        {
            _chests = null;
            _chests = FindObjectsOfType<ExperienceChest>().ToList();
        }
#endif
    }
}
