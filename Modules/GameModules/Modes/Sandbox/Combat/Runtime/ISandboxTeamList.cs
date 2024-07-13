using Agava.Combat;

public interface ISandboxTeamList : ITeamList
{
    public bool TryGetTeam(SandboxTeamType teamType, out ISandboxTeam team);
}
