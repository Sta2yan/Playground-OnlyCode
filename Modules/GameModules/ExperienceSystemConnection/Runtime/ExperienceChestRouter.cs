using System.Collections.Generic;
using System.Linq;
using Agava.AdditionalPredefinedMethods;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.ExperienceSystemConnection
{
    public class ExperienceChestRouter : IGameLoop
    {
        private readonly List<ExperienceChest> _chests;
        private readonly ExperienceEventsContainer _container;

        public ExperienceChestRouter(List<ExperienceChest> chests, ExperienceEventsContainer container)
        {
            _chests = chests;
            _container = container;
        }
        
        public void Update(float deltaTime)
        {
            foreach (var chest in _chests.Where(chest => chest.Activate))
                _container.TriggerEvent(chest.Execute());
        }
    }
}
