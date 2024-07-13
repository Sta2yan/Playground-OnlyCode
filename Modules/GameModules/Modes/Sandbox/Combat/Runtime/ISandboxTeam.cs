using Agava.Combat;

public interface ISandboxTeam : ITeam
{
    void Add(ICombatCharacter combatCharacter);
    void AddFriendlyTeam(ISandboxTeam team);
}
