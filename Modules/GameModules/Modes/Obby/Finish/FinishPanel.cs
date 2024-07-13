using Agava.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.Obby.Finish
{
    public class FinishPanel : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void OnRestartButtonClick()
        {
            _levelList.LoadObby();
        }

        private void OnMenuButtonClick()
        {
            _levelList.LoadMainMenu();
        }
    }
}
