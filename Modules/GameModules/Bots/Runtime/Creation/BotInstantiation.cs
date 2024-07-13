using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class BotInstantiation<T> where T : IBotComposer
    {
        private readonly BotComposition<T> _botComposition;

        public BotInstantiation(BotComposition<T> botComposition)
        {
            _botComposition = botComposition;
        }

        public void InstantiateBot(IBot<T> bot, Vector3 position)
        {
            bot.CombatCharacter.Move(position);
            _botComposition.ComposeBot(bot.BotComposer);
        }
    }
}
