using Agava.AdditionalPredefinedMethods;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class MainMenuBotRouter : IGameLoop, IBotRouter
    {
        private const float MinDelay = 0.5f;
        private const float MaxDelay = 10f;
        private const float FirstDelay = 3f;
            
        private readonly Transform _spawnPosition;
        private readonly BotInstantiation<MainMenuBotComposer> _botInstantiation;

        private List<MainMenuBot> _botsPool;
        private List<MainMenuBot> _usedBots = new();

        private float _lastSpawnTime = 0.0f;
        private float _time = 0.0f;
        private float _nextSpawnDelay = FirstDelay;

        public MainMenuBotRouter(Transform spawnPosition, IEnumerable<MainMenuBot> bots, BotInstantiation<MainMenuBotComposer> botInstantiation)
        {
            _spawnPosition = spawnPosition;
            _botsPool = bots.ToList();
            _botInstantiation = botInstantiation;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_botsPool.Count == 0)
            {
                if (_usedBots.Count == 0)
                    return;

                _botsPool.AddRange(_usedBots);
                _usedBots.Clear();
            }

            if (_time - _lastSpawnTime >= _nextSpawnDelay)
            {
                _nextSpawnDelay = Random.Range(MinDelay, MaxDelay);
                _lastSpawnTime = _time;

                MainMenuBot bot = _botsPool.First();
                _botsPool.Remove(bot);
                _usedBots.Add(bot);

                _botInstantiation.InstantiateBot(bot, _spawnPosition.position);
            }
        }
    }
}
