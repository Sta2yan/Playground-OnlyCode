using Agava.AdditionalPredefinedMethods;
using Agava.Blocks;
using Agava.Input;
using Agava.Playground3D.Bots;
using Agava.Playground3D.Input;
using Agava.Combat;
using UnityEngine;
using Agava.Movement;
using Agava.Playground3D.Sandbox.UserInterface;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.CompositeRoot
{
    public class SandboxBotRoot : CompositeRoot
    {
        [SerializeField] private BlocksRoot _blocksRoot;
        [SerializeField] private Hand _hand;
        [SerializeField] private BlockAnimator _animation;
        [SerializeField] private Transform _botsRoot;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LayerMask _characterLayerMask;
        [SerializeField] private CountView.CountView _botsCountView;
        [SerializeField] private Transform _playerModel;
        [SerializeField] private BotsCommunication _botsCommunication;

        private IInput _input;
        private ISandboxTeamList _teamList;

        private ExperienceEventsContainer _experienceEventsContainer;
        private IItemExperienceEventRule _botSpawnEventRule;
        private IItemExperienceEventRule _botDeathEventRule;

        private IBotRouter _botRouter;
        private IGameLoop _gameLoop;

        public void Initialize(IInput input, ISandboxTeamList teamList,
            ExperienceEventsContainer experienceEventsContainer,
            IItemExperienceEventRule botSpawnEventRule,
            IItemExperienceEventRule botDeathEventRule)
        {
            _input = input;
            _teamList = teamList;
            _experienceEventsContainer = experienceEventsContainer;
            _botSpawnEventRule = botSpawnEventRule;
            _botDeathEventRule = botDeathEventRule;
        }

        public override void Compose()
        {
            SandboxBotComposition botComposition = new SandboxBotComposition(_blocksRoot.BlocksCommunication, _teamList);
            DelayedDestruction delayedDestruction = new DelayedDestruction(this);

            _botRouter = new SandboxBotRouter(_input,
                new BotInstantiation<SandboxBotComposer>(botComposition), _hand,
                _animation, _botsRoot, delayedDestruction,
                _cameraMovement, _layerMask, _botsCountView,
                _playerModel, _characterLayerMask, _botsCommunication,
                _experienceEventsContainer, _botSpawnEventRule, _botDeathEventRule);

            _gameLoop = new GameLoopGroup(_botRouter as IGameLoop);
        }

        public void Update()
        {
            if (_gameLoop != null)
                _gameLoop.Update(Time.deltaTime);
        }
    }
}
