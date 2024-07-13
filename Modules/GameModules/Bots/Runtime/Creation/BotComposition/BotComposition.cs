namespace Agava.Playground3D.Bots
{
    public abstract class BotComposition<T> where T : IBotComposer
    {
        public void ComposeBot(T botComposer)
        {
            InitializeBotComposer(botComposer);
            botComposer.ComposeBot();
        }

        protected abstract void InitializeBotComposer(T botComposer);
    }
}
