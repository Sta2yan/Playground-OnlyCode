using System.Collections;
using System.Collections.Generic;
using Agava.Levels;
using Agava.Save;
#if FIREBASE
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
#endif
#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using Lean.Localization;
using UnityEngine;

namespace Agava.Playground3D.Application
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;
        [SerializeField] private GameObject _appMetrica;

        private bool _saveLoaded;
        private bool _request;

        private IEnumerator Start()
        {
#if YANDEX_GAMES && !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize();

            Dictionary<string, string> languages = new Dictionary<string, string>()
            {
                { "ru", "Language/Russian" },
                { "en", "Language/English" },
                { "tr", "Language/Turkish" }
            };

            var lang = YandexGamesSdk.Environment.i18n.lang;
            LeanLocalization.SetCurrentLanguageAll(languages[lang]);

            SaveFacade.Load(onLoad: () => _saveLoaded = true);
            PlayerAccount.RequestPersonalProfileDataPermission(
                onSuccessCallback: () => _request = true, 
                onErrorCallback: _ => _request = true);

            yield return new WaitUntil(() => _saveLoaded && _request);
            
            _levelList.LoadMainMenu();

            yield break;
#endif

#if ANDROID_BUILD && FIREBASE
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                DependencyStatus dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
#endif

#if ANDROID_BUILD && APPMETRICA
            _appMetrica.SetActive(true);
#else
            _appMetrica.SetActive(false);
#endif

            string language = "Language/English";

#if UNITY_EDITOR
            language = "Language/Russian";
#endif

            LeanLocalization.SetCurrentLanguageAll(language);
            _levelList.LoadMainMenu();
            yield return null;
        }
    }
}
