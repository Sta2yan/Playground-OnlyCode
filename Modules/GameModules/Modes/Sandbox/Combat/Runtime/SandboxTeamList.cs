using Agava.Combat;
using System.Collections.Generic;
using System.Linq;

public class SandboxTeamList : ISandboxTeamList
{
    private readonly Dictionary<SandboxTeamType, ISandboxTeam> _teams;

    public SandboxTeamList(Dictionary<SandboxTeamType, ISandboxTeam> teams)
    {
        _teams = teams;
    }

    public bool PlayerWin => true;

    public bool TryFindCharacterTeam(ICombatCharacter character, out ITeam team)
    {
        team = _teams.FirstOrDefault(team => team.Value.HasCharacter(character)).Value;
        return team != null;
    }

    public bool TryGetFriendlyTeams(ICombatCharacter character, out ITeam[] friendlyTeams)
    {
        friendlyTeams = null;

        if (TryFindCharacterTeam(character, out ITeam targetTeam))
        {
            friendlyTeams = _teams.Values.Where(team => targetTeam.FriendlyTeam(team)).ToArray();
            return true;
        }

        return false;
    }

    public bool TryGetTeam(SandboxTeamType teamType, out ISandboxTeam team)
    {
        bool teamExists = _teams.ContainsKey(teamType);
        team = teamExists ? _teams[teamType] : null;
        return teamExists;
    }
}
