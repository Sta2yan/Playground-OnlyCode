using System;
using System.Collections;
using Agava.ExperienceSystem;
using Agava.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.MainMenu
{
    public class PortalToMode : MonoBehaviour, ILevelGateContent
    {
        [SerializeField] private LevelList _levelList;
        [SerializeField] private LevelsToLoad _levelToLoad;
        [SerializeField] private GameObject _modesPanel;
        [SerializeField] private GameObject _targetMode;
        [SerializeField] private ModesPanel _targetModesPanel;
        [SerializeField] private Button _play;

        [Header("Level gating")]
        [SerializeField] private LockedItem _lockedItem;
        [SerializeField] private GameObject _lockedPortalRoot;
        [SerializeField] private GameObject _unlockedPortalRoot;
        [SerializeField] private LockedItemView _lockedItemView;

        private bool _isIAP = false;
        public string LockedItemId => _lockedItem == null ? null : _lockedItem.Id;
        public bool LevelGated => _lockedItem != null;
        public int UnlockingLevel => _lockedItem == null ? 0 : _lockedItem.UnlockingLevel;
        public bool Unlocked { get; private set; } = true;

        public Action PlayButtonClicked;

        private void Awake()
        {
            _modesPanel.SetActive(false);

            if (_targetModesPanel != null)
                _targetModesPanel.Hide();

            if (_lockedItemView != null)
                _lockedItemView.Render(this);
        }

        private void OnValidate()
        {
            if (_lockedItem != null)
                _lockedItem.Initialize(this);
        }

        private void OnEnable()
        {
            _play.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable()
        {
            _play.onClick.RemoveListener(OnPlayButtonClick);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PortalTarget _))
            {
                if (_modesPanel != null)
                    _modesPanel.SetActive(true);

                if (_targetMode != null)
                    _targetMode.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PortalTarget _))
            {
                if (_modesPanel != null)
                    _modesPanel.SetActive(false);

                if (_targetMode != null)
                    _targetMode.SetActive(false);
            }
        }

        public void SetIAP() => _isIAP = true;


        public void Initialize(GameObject targetMode, GameObject modesPanel)
        {
            _targetMode = targetMode;
            _modesPanel = modesPanel;
        }

        public bool TryUnlock(int currentLevel, bool instaUnlock = false)
        {
            Unlocked = currentLevel >= UnlockingLevel || (LevelGated == false) || instaUnlock;

            _unlockedPortalRoot.SetActive(Unlocked);
            _lockedPortalRoot.SetActive(!Unlocked);

            return Unlocked;
        }

        public void LoadPurchasedLevel()
        {
            LoadLevel(LevelsToLoad.NewYearEvent);
        }

        private void OnPlayButtonClick()
        {
            if(_isIAP)
            {
                PlayButtonClicked?.Invoke();
                return;
            }
            
            if (_targetModesPanel != null)
            {
                _targetModesPanel.Show();
                StartCoroutine(WaitForTargetLevel());
            }
            else
            {
                LoadLevel(_levelToLoad);
            }

        }

        private IEnumerator WaitForTargetLevel()
        {
            if (_targetModesPanel.Closed)
                yield return null;

            yield return new WaitUntil(() => _targetModesPanel.TargetLevel.HasValue);

            LoadLevel(_targetModesPanel.TargetLevel.Value);
        }

        private void LoadLevel(LevelsToLoad level)
        {
            switch (level)
            {
                case LevelsToLoad.Sandbox:
                    _levelList.LoadSandbox();
                    break;
                case LevelsToLoad.BedWars:
                    _levelList.LoadBedWars();
                    break;
                case LevelsToLoad.OnlyUp:
                    _levelList.LoadOnlyUp();
                    break;
                case LevelsToLoad.Obby:
                    _levelList.LoadObby();
                    break;
                case LevelsToLoad.MainMenu:
                    _levelList.LoadMainMenu();
                    break;
                case LevelsToLoad.BedWarsTutorial:
                    _levelList.LoadBedWarsTutorial();
                    break;
                case LevelsToLoad.NewYearEvent:
                    _levelList.LoadNewYearEvent();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Level does not exist!");
            }
        }
    }
}
