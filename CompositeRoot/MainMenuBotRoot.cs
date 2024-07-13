using Agava.Playground3D.Bots;
using Agava.Playground3D.MainMenu;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.Save;
using Agava.AdditionalPredefinedMethods;
using System.Linq;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif

namespace Agava.Playground3D.CompositeRoot
{
    public class MainMenuBotRoot : CompositeRoot
    {
        [SerializeField] private MainMenuBot[] _bots;
        [SerializeField] private BotNicknamesList _nicknamesList;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField, Range(0, 1)] private float _targetUnlockedPortalsChance;

        private Dictionary<string, BotNicknamesList.Language> _languages = new Dictionary<string, BotNicknamesList.Language>()
        {
            { "Language/Russian", BotNicknamesList.Language.RU},
            { "Language/English", BotNicknamesList.Language.EN},
            { "Language/Turkish", BotNicknamesList.Language.TR},
        };

        private IEnumerable<PortalBotTargetSpot> _targetSpots;
        private bool _initialized = false;
        private List<Transform> _targets;

        private IGameLoop _gameLoop;

        public void Initialize(IEnumerable<PortalBotTargetSpot> targetSpots)
        {
            _targetSpots = targetSpots;
            _initialized = true;
        }

        public override void Compose()
        {
            StartCoroutine(ComposeWhenInitialized());
        }

        private IEnumerator ComposeWhenInitialized()
        {
            yield return new WaitUntil(() => _initialized);

            //_targets = _targetSpots == null ? new() : CreateTargetSpotsList(_targetSpots);

            _targets = _targetSpots == null ? new() : _targetSpots.Select(targetSpot => targetSpot.transform).ToList();

            if (_targets.Count == 0)
            {
                foreach (MainMenuBot bot in _bots)
                    Destroy(bot.gameObject);
            }
            else
            {
                List<string> nicknames;

                string language = LeanLocalization.GetFirstCurrentLanguage();

                if (language == null)
                    language = "Language/Russian";

#if YANDEX_GAMES
            Dictionary<string, string> languages = new Dictionary<string, string>()
            {
                { "ru", "Language/Russian" },
                { "en", "Language/English" },
                { "tr", "Language/Turkish" }
            };
                
            language = languages[YandexGamesSdk.Environment.i18n.lang];
#endif

                if (_languages.ContainsKey(language))
                {
                    nicknames = new List<string>(_nicknamesList.Nicknames(_languages[language]));
                }
                else
                {
                    nicknames = new List<string>();
                }

                MainMenuBotComposition composition = new MainMenuBotComposition(nicknames, _targets);
                BotInstantiation<MainMenuBotComposer> botInstantiation = new BotInstantiation<MainMenuBotComposer>(composition);

                IBotRouter botRouter = new MainMenuBotRouter(_spawnPoint, _bots, botInstantiation);
                _gameLoop = new GameLoopGroup(botRouter as IGameLoop);
            }

            yield return null;
        }

        private void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }

        private List<Transform> CreateTargetSpotsList(IEnumerable<PortalBotTargetSpot> targetSpots)
        {
            List<Transform> targets = new();
            List<PortalBotTargetSpot> unlockedPortlas = new();

            int targetPortals = 0;

            foreach (PortalBotTargetSpot targetSpot in targetSpots)
            {
                if (targetSpot.PortalUnlocked)
                {
                    unlockedPortlas.Add(targetSpot);
                }
                else
                {
                    targets.Add(targetSpot.transform);
                    targetPortals++;
                }
            }

            int targetUnlockedPortals = 0;

            if (unlockedPortlas.Count > 0)
            {
                float unlockedPortalChance = 0.0f;
                int portalIndex = 0;

                while (unlockedPortalChance < _targetUnlockedPortalsChance)
                {
                    targets.Add(unlockedPortlas[portalIndex++].transform);

                    if (portalIndex == unlockedPortlas.Count)
                        portalIndex = 0;

                    targetUnlockedPortals++;
                    targetPortals++;

                    unlockedPortalChance = targetUnlockedPortals / (targetPortals * 1.0f);
                }
            }

            return targets;
        }
    }
}
