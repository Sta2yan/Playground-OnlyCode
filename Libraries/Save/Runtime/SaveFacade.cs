using System;

namespace Agava.Save
{
    public static class SaveFacade
    {
        public static void Load(Action onLoad)
        {
            onLoad?.Invoke();

//#if YANDEX_GAMES
//            YandexGames.PlayerPrefs.Load(onSuccessCallback: onLoad, onErrorCallback: _ => onLoad?.Invoke());
//#else
//            onLoad?.Invoke();
//#endif
        }
        
        public static bool HasKey(string key)
        {
            return UnityEngine.PlayerPrefs.HasKey(key);

//#if YANDEX_GAMES
//            return YandexGames.PlayerPrefs.HasKey(key);
//#else
//            return UnityEngine.PlayerPrefs.HasKey(key);
//#endif
        }

        public static void DeleteKey(string key)
        {
            UnityEngine.PlayerPrefs.DeleteKey(key);
            UnityEngine.PlayerPrefs.Save();

//#if YANDEX_GAMES
//            YandexGames.PlayerPrefs.DeleteKey(key);
//            YandexGames.PlayerPrefs.Save();
//#else
//            UnityEngine.PlayerPrefs.DeleteKey(key);
//            UnityEngine.PlayerPrefs.Save();
//#endif
        }

        public static void DeleteAll()
        {
            UnityEngine.PlayerPrefs.DeleteAll();
            UnityEngine.PlayerPrefs.Save();

//#if YANDEX_GAMES
//            YandexGames.PlayerPrefs.DeleteAll();
//            YandexGames.PlayerPrefs.Save();
//#else
//            UnityEngine.PlayerPrefs.DeleteAll();
//            UnityEngine.PlayerPrefs.Save();
//#endif
        }

        public static void SetString(string key, string value)
        {
            UnityEngine.PlayerPrefs.SetString(key, value);
            UnityEngine.PlayerPrefs.Save();

//#if YANDEX_GAMES
//            YandexGames.PlayerPrefs.SetString(key, value);
//            YandexGames.PlayerPrefs.Save();
//#else
//            UnityEngine.PlayerPrefs.SetString(key, value);
//            UnityEngine.PlayerPrefs.Save();
//#endif
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return UnityEngine.PlayerPrefs.GetString(key, defaultValue);

//#if YANDEX_GAMES
//            return YandexGames.PlayerPrefs.GetString(key, defaultValue);
//#else
//            return UnityEngine.PlayerPrefs.GetString(key, defaultValue);
//#endif
        }

        public static void SetInt(string key, int value)
        {
            UnityEngine.PlayerPrefs.SetInt(key, value);
            UnityEngine.PlayerPrefs.Save();

//#if YANDEX_GAMES
//            YandexGames.PlayerPrefs.SetInt(key, value);
//            YandexGames.PlayerPrefs.Save();
//#else
//            UnityEngine.PlayerPrefs.SetInt(key, value);
//            UnityEngine.PlayerPrefs.Save();
//#endif
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return UnityEngine.PlayerPrefs.GetInt(key, defaultValue);

//#if YANDEX_GAMES
//            return YandexGames.PlayerPrefs.GetInt(key, defaultValue);
//#else
//            return UnityEngine.PlayerPrefs.GetInt(key, defaultValue);
//#endif
        }

        public static void SetFloat(string key, float value)
        {
            UnityEngine.PlayerPrefs.SetFloat(key, value);
            UnityEngine.PlayerPrefs.Save();

//#if YANDEX_GAMES
//            YandexGames.PlayerPrefs.SetFloat(key, value);
//            YandexGames.PlayerPrefs.Save();
//#else
//            UnityEngine.PlayerPrefs.SetFloat(key, value);
//            UnityEngine.PlayerPrefs.Save();
//#endif
        }

        public static float GetFloat(string key, float defaultValue = 0f)
        {
            return UnityEngine.PlayerPrefs.GetFloat(key, defaultValue);

//#if YANDEX_GAMES
//            return YandexGames.PlayerPrefs.GetFloat(key, defaultValue);
//#else
//            return UnityEngine.PlayerPrefs.GetFloat(key, defaultValue);
//#endif
        }
    }
}
