using UnityEngine;
using Agava.Playground3D.Bots;
using System.Collections.Generic;
using Agava.YandexGames;
using Lean.Localization;

namespace Agava.Playground3D.CompositeRoot
{
    internal class BedWarsBotRoot : CompositeRoot
    {
        [SerializeField] private BedWarsRoot _bedWarsRoot;
        [SerializeField] private BotNicknamesList _nicknamesList;

        [Header("Map navigation")]
        [SerializeField] private PredefinedPathObject[] _predefinedPaths;
        [SerializeField] private GameObject _middleIsland;
        [SerializeField] private GameObject[] _teamIslands;
        [SerializeField] private GameObject[] _sideIslands;
        [SerializeField] private Transform[] _middleIslandPoints;

        private Dictionary<string, BotNicknamesList.Language> _languages = new Dictionary<string, BotNicknamesList.Language>()
        {
            { "Language/Russian", BotNicknamesList.Language.RU},
            { "Language/English", BotNicknamesList.Language.EN},
        };

        public override void Compose()
        {
            List<string> nicknames;

            string language = LeanLocalization.GetFirstCurrentLanguage();

            if (language == null)
                language = "Language/Russian";

#if YANDEX_GAMES
            var lang = YandexGamesSdk.Environment.i18n.lang;
            language = lang == "ru" ? "Language/Russian" : "Language/English";
#endif

            if (_languages.ContainsKey(language))
            {
                nicknames = new List<string>(_nicknamesList.Nicknames(_languages[language]));
            }
            else
            {
                nicknames = new List<string>();
            }

            var botIslands = _bedWarsRoot.BotIslands;

            BedWarsBotComposition factory = new BedWarsBotComposition(botIslands,
                nicknames,
                _bedWarsRoot.BlocksCommunication,
                _predefinedPaths,
                _middleIsland,
                _teamIslands,
                _sideIslands,
                _middleIslandPoints,
                _bedWarsRoot.TeamList);

            foreach (var bot in botIslands.Keys)
            {
                factory.ComposeBot(bot.BotComposer);
            }
        }
    }
}
