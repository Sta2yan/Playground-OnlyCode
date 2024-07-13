using UnityEngine;
using System.Collections.Generic;

namespace Agava.DroppedItems
{
    public class TakenItemsList : MonoBehaviour
    {
        private readonly Stack<string> _itemIds = new Stack<string>();

        public bool HasItems => _itemIds.Count != 0;

        public string Pop()
        {
            if (HasItems == false)
                throw new System.InvalidOperationException();

            return _itemIds.Pop();
        }
        
        internal void Add(string itemId)
        {
            _itemIds.Push(itemId);
        }
    }
}
