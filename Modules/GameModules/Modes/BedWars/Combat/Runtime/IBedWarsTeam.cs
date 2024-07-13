using Agava.Combat;

namespace Agava.Playground3D.BedWars.Combat
{
    public interface IBedWarsTeam : ITeam
    {
        bool Alive { get; }
    }
}