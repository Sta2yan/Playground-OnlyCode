using Agava.Combat;
using System.Collections.Generic;

public class SandboxTeam : ISandboxTeam
{
    private List<ICombatCharacter> _characters;
    private List<ITeam> _friendlyTeams;

    public SandboxTeam()
    {
        _characters = new List<ICombatCharacter>();
        _friendlyTeams = new List<ITeam>();
    }

    public void Add(ICombatCharacter combatCharacter)
    {
        if (_characters.Contains(combatCharacter) == false)
        {
            _characters.Add(combatCharacter);
        }
    }

    public void AddFriendlyTeam(ISandboxTeam team)
    {
        if (_friendlyTeams.Contains(team) == false)
        {
            _friendlyTeams.Add(team);
        }
    }

    public bool FriendlyTeam(ITeam team)
    {
        return _friendlyTeams.Contains(team);
    }

    public bool HasCharacter(ICombatCharacter combatCharacter)
    {
        return _characters.Contains(combatCharacter);
    }
}
