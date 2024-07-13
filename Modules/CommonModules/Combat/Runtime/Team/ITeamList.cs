namespace Agava.Combat
{
    public interface ITeamList
    {
        bool PlayerWin { get; }
        bool TryFindCharacterTeam(ICombatCharacter character, out ITeam team);
        bool TryGetFriendlyTeams(ICombatCharacter character, out ITeam[] friendlyTeams);
    }
}
