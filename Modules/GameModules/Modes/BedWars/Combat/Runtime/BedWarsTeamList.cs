using Agava.Combat;
using System.Collections.Generic;
using System.Linq;

namespace Agava.Playground3D.BedWars.Combat
{
    public class BedWarsTeamList : IBedWarsTeamList
    {
        private readonly IBedWarsTeam _playerTeam;
        private readonly List<IBedWarsTeam> _teams;

        public BedWarsTeamList(IEnumerable<IBedWarsTeam> teams, IBedWarsTeam playerTeam)
        {
            _playerTeam = playerTeam;
            _teams = new List<IBedWarsTeam>(teams);
        }

        public bool GameOver => _teams.Count(team => team.Alive) == 1;
        public bool PlayerWin => GameOver && _teams.First(team => team.Alive) == _playerTeam;
        public bool PlayerLose => GameOver && _teams.First(team => team.Alive) != _playerTeam;

        public bool TryFindCharacterTeam(ICombatCharacter character, out ITeam team)
        {
            team = _teams.FirstOrDefault(team => team.HasCharacter(character));
            return team != null;
        }

        public bool TryGetFriendlyTeams(ICombatCharacter character, out ITeam[] friendlyTeams)
        {
            friendlyTeams = null;

            if (TryFindCharacterTeam(character, out ITeam targetTeam))
            {
                friendlyTeams = _teams.Where(team => targetTeam.FriendlyTeam(team)).ToArray();
                return true;
            }

            return false;
        }
    }
}
