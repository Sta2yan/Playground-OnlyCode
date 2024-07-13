using Agava.Levels;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.MainMenu
{
    internal class ModesPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _rootObject;
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private ModeButton[] _modeButtons;

        public LevelsToLoad? TargetLevel { get; private set; } = null;
        public bool Closed { get; private set; } = true;

        private void OnEnable()
        {
            foreach (ModeButton modeButton in _modeButtons)
                modeButton.Button.onClick.AddListener(() => TargetLevel = modeButton.Level);

            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            foreach (ModeButton modeButton in _modeButtons)
                modeButton.Button.onClick.RemoveListener(() => TargetLevel = modeButton.Level);

            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            TargetLevel = null;
            Closed = false;
            _rootObject.SetActive(true);
        }

        public void Hide()
        {
            TargetLevel = null;
            Closed = true;
            _rootObject.SetActive(false);
        }
    }

    [Serializable]
    internal struct ModeButton
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public LevelsToLoad Level { get; private set; }
    }
}
