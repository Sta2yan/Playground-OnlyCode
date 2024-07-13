using Agava.Playground3D.Bots;
using Agava.Playground3D.NewYearEvent;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class NewYearEventBotRoot : CompositeRoot
    {
        [SerializeField] private NewYearEventBot[] _bots;

        private CollectableItemsBotContainer _container;

        public void Initialize(CollectableItemsBotContainer container)
        {
            _container = container;
        }

        public override void Compose()
        {
            NewYearEventBotComposition newYearEventBotComposition = new NewYearEventBotComposition(_container);

            foreach (NewYearEventBot bot in _bots)
                newYearEventBotComposition.ComposeBot(bot.BotComposer);
        }

    }
}
