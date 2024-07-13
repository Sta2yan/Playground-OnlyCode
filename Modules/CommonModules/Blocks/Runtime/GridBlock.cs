using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Agava.Blocks
{
    internal class GridBlock
    {
        private readonly GameObject _block;

        public GridBlock(string itemId, int maxHealth, Vector3Int size, GameObject block, Vector3Int[] occupiedPositions, GameObject interactableObject, bool triggerCollider, bool setupBlock = false)
        {
            _block = block;
            ItemId = itemId;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Size = size;
            OccupiedPositions = occupiedPositions;
            InteractableObject = interactableObject;
            TriggerCollider = triggerCollider;
            SetupBlock = setupBlock;
        }

        public string ItemId { get; }
        public int MaxHealth { get; }
        public Vector3Int Size { get; }
        public Vector3Int[] OccupiedPositions { get; }
        public GameObject InteractableObject { get; }
        public bool TriggerCollider { get; }
        public int CurrentHealth { get; private set; }
        public bool SetupBlock { get; private set; }
        public bool Broken => CurrentHealth <= 0;

        public void ApplyDamage(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value) + " cant be less 0!");

            CurrentHealth -= value;

            if (Broken && _block != null)
                Object.Destroy(_block);
        }
    }
}
