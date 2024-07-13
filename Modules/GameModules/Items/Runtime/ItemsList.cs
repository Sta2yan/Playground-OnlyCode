using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Items
{
    [CreateAssetMenu(menuName = "Items/Create ItemsList", fileName = "ItemsList", order = 56)]
    public class ItemsList : ScriptableObject
    {
        private readonly Dictionary<string, IItem> _itemsWithId = new();

        [SerializeField] private Item[] _items;
        [NonSerialized] private bool _initialized;

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            foreach (var item in _items)
            {
#if !UNITY_EDITOR
    if (item.DebugItem)
        continue;
#endif

                _itemsWithId.Add(item.Id, item);
            }
        }

        public bool TryGetItemById(string id, out IItem item)
        {
            return _itemsWithId.TryGetValue(id, out item);
        }
    }
}
