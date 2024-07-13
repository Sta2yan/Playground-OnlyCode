using TMPro;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class CollectedItemsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;

        public int ItemsCount { get; private set; } = 0;

        public void UpdateValue(int newValue)
        {
            ItemsCount = newValue;
            _count.text = ItemsCount.ToString();
        }
    }
}
