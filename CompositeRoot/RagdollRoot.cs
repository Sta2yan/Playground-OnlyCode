using Agava.AdditionalPredefinedMethods;
using Agava.ExperienceSystem;
using Agava.Input;
using Agava.Movement;
using Agava.Playground3D.GravyGun;
using Agava.Playground3D.Input;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class RagdollRoot : CompositeRoot
    {
        [SerializeField] private GameObject _pointer;
        [SerializeField] private LayerMask _charactersMask;
        [SerializeField] private LayerMask _collisionsMask;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private GameObject _pivot;
        [SerializeField] private LayerMask _attackLayers;

        private IInput _input;
        private Hand _hand;
        private bool _mobile;
        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _gravityGunUseEventRule;

        private IGameLoop _gameLoop;

        public void Initialize(IInput input, Hand hand, bool mobile,
            ExperienceEventsContainer experienceEventsContainer,
            ExperienceEventRule gravityGunUseEventRule)
        {
            _input = input;
            _hand = hand;
            _mobile = mobile;
            _experienceEventsContainer = experienceEventsContainer;
            _gravityGunUseEventRule = gravityGunUseEventRule;
        }

        public override void Compose()
        {
            RagdollRouter gravyGunRouter = new RagdollRouter(_input, _hand, _pointer, _charactersMask, _collisionsMask, _cameraMovement, _mobile, _experienceEventsContainer, _gravityGunUseEventRule, _pivot, _attackLayers);
            _gameLoop = new GameLoopGroup(gravyGunRouter as IGameLoop);
        }

        public void Update()
        {
            if (_gameLoop != null)
                _gameLoop.Update(Time.deltaTime);
        }
    }
}
