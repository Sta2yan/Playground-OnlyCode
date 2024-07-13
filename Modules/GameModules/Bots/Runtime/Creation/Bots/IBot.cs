using Agava.Combat;

namespace Agava.Playground3D.Bots
{
    public interface IBot<T> where T : IBotComposer
    {
        public T BotComposer { get; }
        public CombatCharacter CombatCharacter { get; }
    }
}
