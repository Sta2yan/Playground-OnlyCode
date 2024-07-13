using Agava.Blocks;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Agava.Playground3D.BedWars.Combat;

namespace Agava.Playground3D.Bots
{
    internal class BedWarsBotComposition : BotComposition<BedWarsBotComposer>
    {
        private readonly IReadOnlyDictionary<BedWarsBot, GameObject> _botIslands;
        private readonly List<string> _nicknames;
        private readonly BlocksCommunication _blocksCommunication;
        private readonly PredefinedPathObject[] _predefinedPathObjects;
        private readonly GameObject _middleIsland;
        private readonly GameObject[] _teamIslands;
        private readonly GameObject[] _sideIslands;
        private readonly Transform[] _middleIslandPoints;
        private readonly IBedWarsTeamList _teamList;

        public BedWarsBotComposition(IReadOnlyDictionary<BedWarsBot, GameObject> botIslands,
            IEnumerable<string> nicknames,
            BlocksCommunication blocksCommunication,
            PredefinedPathObject[] predefinedPathObjects,
            GameObject middleIsland,
            GameObject[] teamIslands,
            GameObject[] sideIslands,
            Transform[] middleIslandPoints, 
            IBedWarsTeamList teamList) : base()
        {
            _botIslands = botIslands;
            _nicknames = nicknames.ToList();
            _blocksCommunication = blocksCommunication;
            _predefinedPathObjects = predefinedPathObjects;
            _middleIsland = middleIsland;
            _teamIslands = teamIslands;
            _sideIslands = sideIslands;
            _middleIslandPoints = middleIslandPoints;
            _teamList = teamList;
        }

        protected override void InitializeBotComposer(BedWarsBotComposer botComposer)
        {
            var pair = _botIslands.FirstOrDefault(pair => pair.Key.BotComposer == botComposer);

            string randomNickname;

            if (_nicknames.Count > 0)
            {
                randomNickname = _nicknames[Random.Range(0, _nicknames.Count - 1)];
                _nicknames.Remove(randomNickname);
            }
            else
            {
                randomNickname = string.Empty;
            }

            botComposer.Initialize(randomNickname,
                _blocksCommunication,
                _predefinedPathObjects,
                pair.Value,
                _middleIsland,
                _teamIslands,
                _sideIslands,
                _middleIslandPoints,
                _teamList);
        }
    }
}
