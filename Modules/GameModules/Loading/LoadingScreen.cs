using UnityEngine;
using Agava.Levels;
using Agava.Utils;
using System.Collections.Generic;

namespace Agava.Playground3D.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private LevelList _levelList;

        [Header("Backgrounds")]
        [SerializeField] private GameObject _mainMenuBackground;
        [SerializeField] private GameObject _sandboxLevelBackground;
        [SerializeField] private GameObject _bedWarsLevelBackground;
        [SerializeField] private GameObject _onlyUpLevelBackground;
        [SerializeField] private GameObject _obbyLevelBackground;
        [SerializeField] private GameObject _newYearEventLevelBackground;

        [Header("UI")]
        [SerializeField] private GameObject _progressBar;
        [SerializeField] private SlicedFilledImage _progressBarImage;

        private bool _loading = false;
        private float _progress;
        private GameObject _currentBackground;
        private Dictionary<LevelsToLoad, GameObject> _levelBackgrounds;

        private void Awake()
        {
            _currentBackground = _mainMenuBackground;

            _mainMenuBackground.SetActive(false);
            _sandboxLevelBackground.SetActive(false);
            _bedWarsLevelBackground.SetActive(false);
            _onlyUpLevelBackground.SetActive(false);
            _obbyLevelBackground.SetActive(false);

            _progressBar.gameObject.SetActive(false);

            _levelBackgrounds = new()
            {
                { LevelsToLoad.MainMenu, _mainMenuBackground},
                { LevelsToLoad.Sandbox, _sandboxLevelBackground},
                { LevelsToLoad.Obby, _obbyLevelBackground},
                { LevelsToLoad.OnlyUp, _onlyUpLevelBackground},
                { LevelsToLoad.BedWars, _bedWarsLevelBackground},
                { LevelsToLoad.BedWarsTutorial, _bedWarsLevelBackground},
                { LevelsToLoad.NewYearEvent, _newYearEventLevelBackground}
            };
        }

        private void Update()
        {
            bool loading = _levelList.LoadingOperation != null;

            if ((loading ^ _loading) == false)
            {
                if (loading == false)
                    return;
            }
            else
            {
                _loading = loading;

                if (_loading)
                {
                    LevelsToLoad loadingLevel = _levelList.LoadingLevel;

                    if (_levelBackgrounds.ContainsKey(loadingLevel))
                        _currentBackground = _levelBackgrounds[loadingLevel];
                }

                _currentBackground.SetActive(_loading);
                _progressBar.gameObject.SetActive(_loading);
            }

            float progress;

            if (_loading)
            {
                progress = _levelList.LoadingOperation.progress;

                if (progress > _progress)
                {
                    _progress = progress;
                    _progressBarImage.fillAmount = progress;
                }
            }
        }
    }
}
