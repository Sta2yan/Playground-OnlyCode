namespace Agava.Combat
{
    public interface ITeam
    {
        public bool HasCharacter(ICombatCharacter combatCharacter);

        bool FriendlyTeam(ITeam team);
    }
}
