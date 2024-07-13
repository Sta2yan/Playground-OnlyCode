using System;
using UnityEngine;

namespace Agava.Customization
{
    [Serializable]
    internal class Skin
    {
        [SerializeField] private string _id;
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private bool _without;

        [Header("Unlockable")]
        [SerializeField] private string _skinEnabledSaveKey = string.Empty;

        public string Id => _id;
        public bool Without => _without;
        public string SkinEnabledSaveKey => _skinEnabledSaveKey;

        public void Enable()
        {
            foreach (var meshFilter in _renderers)
                meshFilter.gameObject.SetActive(true);
        }

        public void Disable()
        {
            try
            {
                foreach (var meshFilter in _renderers)
                    meshFilter.gameObject.SetActive(false);
            }
            catch (NullReferenceException)
            {
                Debug.Log(Id);
            }
        }
    }
}