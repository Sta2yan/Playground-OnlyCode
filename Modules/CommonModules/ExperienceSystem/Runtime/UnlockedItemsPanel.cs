using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.ExperienceSystem
{
    public class UnlockedItemsPanel : MonoBehaviour
    {
        [SerializeField] private Transform _lockedItemViewsRoot;
        [SerializeField] private UnlockedItemView _unlockedItemViewTemplate;

        private List<UnlockedItemView> _views = new();

        public void Render(LockedItem[] items)
        {
            if (_views.Count > 0)
            {
                foreach (UnlockedItemView view in _views)
                    Destroy(view.gameObject);

                _views.Clear();
            }

            foreach (LockedItem unlockedItem in items)
            {
                UnlockedItemView view = Instantiate(_unlockedItemViewTemplate, _lockedItemViewsRoot);
                _views.Add(view);
                view.Render(unlockedItem);
                //RecalculatingHeight();
            }
        }

        private void RecalculatingHeight()
        {
            float childrenHeight = 0f;
            int childCount = transform.childCount;

            for (int i = 0; i < childCount; i++)
                childrenHeight += (transform.GetChild(i) as RectTransform).rect.height;

            var rectTransform = transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, childrenHeight);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_lockedItemViewsRoot as RectTransform);
        }
    }
}
