using Agava.Blocks;
using Agava.ExperienceSystem;
using Agava.Playground3D.Dynamite;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class DynamiteRoot : CompositeRoot
    {
        [SerializeField] private DynamiteContainer _dynamiteContainer;
        [SerializeField] private BlocksRoot _blocksRoot;

        private BlocksCommunication _blocksCommunication;

        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _dynamiteExplosionEventRule;

        public void Initialize(ExperienceEventsContainer experienceEventsContainer,
            ExperienceEventRule dynamiteExplosionEventRule)
        {
            _experienceEventsContainer = experienceEventsContainer;
            _dynamiteExplosionEventRule = dynamiteExplosionEventRule;
        }

        public override void Compose()
        {
            _blocksCommunication = _blocksRoot.BlocksCommunication;
            _dynamiteContainer.Initialize(_blocksCommunication, _experienceEventsContainer, _dynamiteExplosionEventRule);
        }
    }
}
