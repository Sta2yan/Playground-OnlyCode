using Agava.Time;
using TMPro;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class FinishPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _itemsCount;
        [SerializeField] private TimeCountView _timeCountView;
        [SerializeField] private CollectedItemsView _collectedItemsView;

        public void Show()
        {
            _timeCountView.Stop();
            _time.text = ConvertToTimeText(_timeCountView.Value);
            _itemsCount.text = _collectedItemsView.ItemsCount.ToString();
            _root.SetActive(true);
        }

        public void Hide()
        {
            _root.SetActive(false);
        }

        private string ConvertToTimeText(float value)
        {
            return $"{(int)value / 60:00}:{(int)value % 60:00}";
        }
    }
}
