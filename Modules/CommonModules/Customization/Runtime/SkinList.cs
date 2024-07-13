using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Agava.Save;

namespace Agava.Customization
{
    public class SkinList : MonoBehaviour
    {
        [SerializeField] private string _saveKey;
        private readonly List<Vector3> _defaultScales = new();

        [SerializeField] private List<Skin> _skins;
        [SerializeField] private List<GameObject> _partsThirdPerson;
        [SerializeField, Tooltip("Can be null")] private SkinList _without;

        [Header("UnlockableSkins")]
        [SerializeField] private List<Skin> _unlockableSkins;
        [SerializeField] public bool _allSkinsUnlocked = true;

        private int _currentSkinIndex;
        private int _chosenSkinIndex;

        public bool CurrentSkinChosen => _chosenSkinIndex == _currentSkinIndex;
        internal Skin CurrentSkin => _skins[_currentSkinIndex];

        private void Start()
        {
            foreach (var unlockableSkin in _unlockableSkins)
            {
                string key = unlockableSkin.SkinEnabledSaveKey;

                bool addSkin = string.IsNullOrEmpty(key) || SaveFacade.HasKey(key);

#if UNITY_EDITOR
                addSkin |= _allSkinsUnlocked;
#endif

                if (addSkin)
                    _skins.Add(unlockableSkin);
            }

            string chosenSkinId = _skins[_currentSkinIndex].Id;
            _chosenSkinIndex = _currentSkinIndex;

            if (SaveFacade.HasKey(_saveKey))
            {
                string savedSkinId = SaveFacade.GetString(_saveKey, _skins[_currentSkinIndex].Id);

                if (savedSkinId != chosenSkinId && _skins.Any(skin => skin.Id == savedSkinId))
                {
                    chosenSkinId = savedSkinId;
                    _currentSkinIndex = _skins.IndexOf(_skins.First(skin => skin.Id == chosenSkinId));
                    _chosenSkinIndex = _currentSkinIndex;
                }
            }

            EnableSkinBy(chosenSkinId);
            EnableSkinBy(_skins[_currentSkinIndex].Id);

            foreach (var partThirdPerson in _partsThirdPerson)
                _defaultScales.Add(partThirdPerson.transform.localScale);
        }

        public void Next()
        {
            int nextIndex = _currentSkinIndex + 1;
            _currentSkinIndex = nextIndex >= _skins.Count ? 0 : nextIndex;

            EnableSkinBy(_skins[_currentSkinIndex].Id);
        }

        public void Previous()
        {
            int previousIndex = _currentSkinIndex - 1;
            _currentSkinIndex = previousIndex < 0 ? _skins.Count - 1 : previousIndex;

            EnableSkinBy(_skins[_currentSkinIndex].Id);
        }

        public void ChoseCurrentSkin()
        {
            _chosenSkinIndex = _currentSkinIndex;
            SaveFacade.SetString(_saveKey, _skins[_chosenSkinIndex].Id);
        }

        public void ChooseRandomSkin()
        {
            _chosenSkinIndex = Random.Range(0, _skins.Count);
            EnableChosenSkin();
        }

        public void EnableChosenSkin()
        {
            _currentSkinIndex = _chosenSkinIndex;
            EnableSkinBy(_skins[_chosenSkinIndex].Id);
        }

        public void EnablePartsThirdPerson()
        {
            foreach (var partThirdPerson in _partsThirdPerson)
                foreach (var defaultScale in _defaultScales)
                    partThirdPerson.transform.localScale = defaultScale;
        }

        public void DisablePartsThirdPerson()
        {
            foreach (var partThirdPerson in _partsThirdPerson)
                partThirdPerson.transform.localScale = Vector3.zero;
        }

        public void DisableAll()
        {
            foreach (var skin in _skins)
                skin.Disable();
        }

        private void EnableSkinBy(string id)
        {
            foreach (var skin in _skins)
            {
                if (skin.Id == id)
                {
                    skin.Enable();

                    if (_without != null)
                    {
                        StartCoroutine(CorrectAfterTime(skin));
                    }
                }
                else
                {
                    skin.Disable();
                }
            }
        }

        private IEnumerator CorrectAfterTime(Skin skin)
        {
            yield return new WaitForSecondsRealtime(.1f);

            if (skin.Without)
                _without.DisableAll();
            else
                _without.EnableChosenSkin();
        }
    }
}
