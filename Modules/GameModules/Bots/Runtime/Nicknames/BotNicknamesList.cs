using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    [CreateAssetMenu(fileName = "BotNicknamesList", menuName = "Bots/NicknamesList", order = 56)]
    public class BotNicknamesList : ScriptableObject
    {
        [SerializeField] private string[] _russianNicknames;
        [SerializeField] private string[] _englishNicknames;
        [SerializeField] private string[] _turkishNicknames;

        private Dictionary<Language, string[]> _localization;

        public IReadOnlyCollection<string> Nicknames(Language language)
        {
            _localization = new Dictionary<Language, string[]>()
            {
                { Language.RU, _russianNicknames},
                { Language.EN, _englishNicknames},
                { Language.TR, _turkishNicknames},
            };

            if (_localization.ContainsKey(language))
                return _localization[language];

            return new string[0];
        }

        public enum Language
        {
            RU,
            EN,
            TR
        }
    }
}
