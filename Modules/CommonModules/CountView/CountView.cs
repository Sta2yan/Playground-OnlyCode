using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Agava.CountView
{
    public class CountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private GameObject _container;
        [SerializeField] private Color _regularColor;
        [SerializeField] private Color _limitColor;

        private int _maxCount;
        private int _count;
        private void OnValidate()
        {
            _countText.color = _regularColor;
        }

        public void Initialize(int maxCount)
        {
            _maxCount = maxCount;
        }

        public void SetCount(int value)
        {
            _count = value;
            _countText.color = _count >= _maxCount ? _limitColor : _regularColor;
            ChangeText();
        }

        public void SetActive(bool active)
        {
            _container.SetActive(active);
        }

        private void ChangeText()
        {
            _countText.text = $"{_count}/{_maxCount}";
        }
    }
}
