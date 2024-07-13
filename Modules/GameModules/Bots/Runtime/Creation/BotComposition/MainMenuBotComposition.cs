using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class MainMenuBotComposition : BotComposition<MainMenuBotComposer>
    {
        private readonly List<string> _nicknames;
        private readonly List<Transform> _targetSpots;

        private List<string> _usedNicknames = new();
        private List<Transform> _usedTargetSpots = new();

        public MainMenuBotComposition(List<string> nicknames, List<Transform> targetSpots)
        {
            _nicknames = nicknames;
            _targetSpots = targetSpots;
        }

        protected override void InitializeBotComposer(MainMenuBotComposer botComposer)
        {
            string randomNickname = RandomObject(_nicknames, _usedNicknames, string.Empty);
            Transform randomTarget = RandomObject(_targetSpots, _usedTargetSpots, null);

            botComposer.Initialize(randomNickname, randomTarget);
        }

        private T RandomObject<T>(List<T> targetObjectsList, List<T> usedObjectsList, T defaultValue)
        {
            T randomObject = defaultValue;

            if (targetObjectsList.Count == 0)
            {
                if (usedObjectsList.Count > 0)
                {
                    targetObjectsList.AddRange(usedObjectsList);
                    usedObjectsList.Clear();
                }
            }

            if (targetObjectsList.Count > 0)
            {
                randomObject = targetObjectsList[Random.Range(0, targetObjectsList.Count - 1)];
                targetObjectsList.Remove(randomObject);
                usedObjectsList.Add(randomObject);
            }

            return randomObject;
        }
    }
}
