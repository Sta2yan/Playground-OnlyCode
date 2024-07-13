using Agava.Combat;

namespace Agava.Playground3D.BedWars.Combat
{
    public interface IBedWarsTeamList : ITeamList
    {
        bool GameOver { get; }
        bool PlayerLose { get; }
    }
}
