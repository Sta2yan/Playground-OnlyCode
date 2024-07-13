using System;
using System.Collections;
using Agava.Utils;
using TMPro;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class ExperienceSystemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _experience;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _levelUpView;
        [SerializeField] private float _delayTimeToDisableLevelReceivedPanel;
        [SerializeField] private GameObject _newLevelReceivedPanel;
        [SerializeField] private SlicedFilledImage _imageToFillExperience;
        [SerializeField] private Vector3 _upScaleExperienceCollect = new(1.3f, 1.3f, 1.3f);
        [SerializeField] private float _speedChangeScaleExperienceCollect = .01f;
        [SerializeField] private float _speedFill;

        [Header("Locked items")]
        [SerializeField] private UnlockedItemsPanel _unlockedItemsPanel;

        private LockedItemsList _lockedItemsList;
        private LevelExperiencePlayer _levelExperiencePlayer;
        private ExperiencePlayer _experiencePlayer;
        private WaitForSecondsRealtime _delay;
        private Coroutine _scaleExperienceTextCoroutine;
        private Coroutine _scaleLevelTextCoroutine;
        private int _currentExperience;
        private int _currentLevel;

        private void Start()
        {
            _currentExperience = _levelExperiencePlayer.ExperienceValue;
            _currentLevel = _levelExperiencePlayer.Value;
            _newLevelReceivedPanel.SetActive(false);
            _delay = new WaitForSecondsRealtime(_delayTimeToDisableLevelReceivedPanel);
        }

        private void Update()
        {
            _experience.text = _levelExperiencePlayer.ExperienceValue.ToString();
            _level.text = _levelExperiencePlayer.Value.ToString();
            _levelUpView.text = _levelExperiencePlayer.Value.ToString();

            float targetFillAmount = _levelExperiencePlayer.LastLevel == false
                ? (float)(_levelExperiencePlayer.ExperienceValue - _levelExperiencePlayer.ExperienceCurrentLevel) /
                  (_levelExperiencePlayer.ExperienceToNextLevel - _levelExperiencePlayer.ExperienceCurrentLevel)
                : 1;

            _imageToFillExperience.fillAmount = Mathf.MoveTowards(_imageToFillExperience.fillAmount, targetFillAmount, _speedFill * Time.deltaTime);

            if (_levelExperiencePlayer.Value > _currentLevel)
            {
                _currentLevel = _levelExperiencePlayer.Value;

                if (_lockedItemsList.TryGetItemsByLevel(_currentLevel, out LockedItem[] items))
                {
                    _unlockedItemsPanel.Render(items);
                    _unlockedItemsPanel.gameObject.SetActive(true);
                }
                else
                {
                    _unlockedItemsPanel.gameObject.SetActive(false);
                }

                StartCoroutine(NewLevelReceived());

                if (_scaleLevelTextCoroutine != null)
                {
                    StopCoroutine(_scaleLevelTextCoroutine);
                    _experience.transform.localScale = Vector3.one;
                }

                _scaleLevelTextCoroutine = StartCoroutine(NewExperienceCollect(_level.transform));
            }

            if (_levelExperiencePlayer.ExperienceValue > _currentExperience)
            {
                _currentExperience = _levelExperiencePlayer.ExperienceValue;

                if (_scaleExperienceTextCoroutine != null)
                {
                    StopCoroutine(_scaleExperienceTextCoroutine);
                    _experience.transform.localScale = Vector3.one;
                }

                _scaleExperienceTextCoroutine = StartCoroutine(NewExperienceCollect(_experience.transform));
            }
        }

        public void Initialize(LockedItemsList lockedItemsList, LevelExperiencePlayer levelExperiencePlayer, ExperiencePlayer experiencePlayer)
        {
            _lockedItemsList = lockedItemsList;
            _levelExperiencePlayer = levelExperiencePlayer;
            _experiencePlayer = experiencePlayer;
        }

        private IEnumerator NewExperienceCollect(Transform transformSelf)
        {
            Vector3 defaultExperienceLocalScale = transformSelf.localScale;

            while (transformSelf.localScale != _upScaleExperienceCollect)
            {
                float stepScale = _speedChangeScaleExperienceCollect * Time.timeScale;

                transformSelf.localScale =
                    Vector3.MoveTowards(transformSelf.localScale, _upScaleExperienceCollect, stepScale);

                yield return null;
            }

            while (transformSelf.localScale != defaultExperienceLocalScale)
            {
                float stepScale = _speedChangeScaleExperienceCollect * Time.timeScale;

                transformSelf.localScale =
                    Vector3.MoveTowards(transformSelf.localScale, defaultExperienceLocalScale, stepScale);

                yield return null;
            }
        }

        private IEnumerator NewLevelReceived()
        {
            _newLevelReceivedPanel.SetActive(true);

            yield return _delay;

            _newLevelReceivedPanel.SetActive(false);
        }
    }
}
