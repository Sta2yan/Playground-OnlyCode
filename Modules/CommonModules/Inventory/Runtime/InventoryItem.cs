using System;
using UnityEngine;

namespace Agava.Inventory
{
    public readonly struct InventoryItem : IEquatable<InventoryItem>
    {
        public string Id { get; }
        public int MaxCount { get; }
        public Sprite Sprite { get; }

        public InventoryItem(string id, int maxCount = 0, Sprite sprite = null)
        {
            Id = id;
            MaxCount = maxCount;
            Sprite = sprite;
        }

        public bool Equals(InventoryItem other)
        {
            return Id == other.Id;
        }
    }
}