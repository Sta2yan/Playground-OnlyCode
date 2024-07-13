using Agava.Playground3D.NewYearEvent;

namespace Agava.Playground3D.Bots
{
    public class NewYearEventBotComposition : BotComposition<NewYearEventBotComposer>
    {
        private CollectableItemsBotContainer _container;

        public NewYearEventBotComposition(CollectableItemsBotContainer container)
        {
            _container = container;
        }

        protected override void InitializeBotComposer(NewYearEventBotComposer botComposer)
        {
            botComposer.Initialize(_container);
        }
    }
}
