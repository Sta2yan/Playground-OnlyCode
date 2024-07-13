using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agava.Levels
{
    [CreateAssetMenu(menuName = "Create LevelList", fileName = "LevelList", order = 56)]
    public class LevelList : ScriptableObject
    {
        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private string _bedWarsSceneName;
        [SerializeField] private string _onlyUpSceneName;
        [SerializeField] private string _sandboxSceneName;
        [SerializeField] private string _obbySceneName;
        [SerializeField] private string _bedWarsTutorialSceneName;
        [SerializeField] private string _newYearEventSceneName;

        public AsyncOperation LoadingOperation { get; private set; }
        public LevelsToLoad LoadingLevel { get; private set; }

        public void LoadMainMenu()
        {
            LoadingLevel = LevelsToLoad.MainMenu;
            LoadScene(_mainMenuSceneName);
        }

        public void LoadBedWars()
        {
            LoadingLevel = LevelsToLoad.BedWars;
            LoadScene(_bedWarsSceneName);
        }

        public void LoadBedWarsTutorial()
        {
            LoadingLevel = LevelsToLoad.BedWarsTutorial;
            LoadScene(_bedWarsTutorialSceneName);
        }

        public void LoadOnlyUp()
        {
            LoadingLevel = LevelsToLoad.OnlyUp;
            LoadScene(_onlyUpSceneName);
        }

        public void LoadSandbox()
        {
            LoadingLevel = LevelsToLoad.Sandbox;
            LoadScene(_sandboxSceneName);
        }

        public void LoadObby()
        {
            LoadingLevel = LevelsToLoad.Obby;
            LoadScene(_obbySceneName);
        }

        public void LoadNewYearEvent()
        {
            LoadingLevel = LevelsToLoad.NewYearEvent;
            LoadScene(_newYearEventSceneName);
        }

        private void LoadScene(string sceneName)
        {
            Time.timeScale = 1;
            LoadingOperation = SceneManager.LoadSceneAsync(sceneName);
            LoadingOperation.completed += OnLoadingOperationCompleted;
        }

        private void OnLoadingOperationCompleted(AsyncOperation asyncOperation)
        {
            LoadingOperation.completed -= OnLoadingOperationCompleted;
            LoadingOperation = null;
        }
    }
}
