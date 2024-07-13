using System;
using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Blocks
{
    public class GridSetup : MonoBehaviour
    {
        private readonly IItem _nullableItem = new NullableItem();

        [SerializeField] private Block[] _blocks;

        private BlocksCommunication _blocksCommunication;
        private ItemsList _itemsList;

        public void Initialize(BlocksCommunication blocksCommunication, ItemsList itemsList)
        {
            if (_blocksCommunication != null)
                throw new InvalidOperationException(nameof(blocksCommunication) + " can't be Initialized 2 times!");

            _blocksCommunication = blocksCommunication;
            _itemsList = itemsList;
        }

        public void UploadBlocks()
        {
            foreach (var block in _blocks)
            {
                bool hasId = string.IsNullOrEmpty(block.ItemId) == false;
                int health = int.MaxValue;

                if (hasId && _itemsList.TryGetItemById(block.ItemId, out var item) && item.TryConvertTo(out IBlock blockItem))
                    health = blockItem.Health;

                if (_blocksCommunication.TryPlace(block, block.transform.position, hasId ? block.ItemId : _nullableItem.Id,
                        health, withRotation: true, hasId ? block.gameObject : null, setupBlock: true) == false)
                    Debug.LogException(new InvalidOperationException($"Positions intersect for: {block}"), block);
            }
        }

#if UNITY_EDITOR
        [ContextMenu(nameof(SetupBlocks))]
        private void SetupBlocks()
        {
            _blocks = Array.Empty<Block>();
            var blocks = FindObjectsOfType<Block>(false);

            if (_blocks == null || _blocks.Length < blocks.Length)
                _blocks = blocks;

            foreach (var block in _blocks)
                if (block == null)
                    _blocks = blocks;
        }

        [ContextMenu(nameof(DestroyBlocksInBlock))]
        private void DestroyBlocksInBlock()
        {
            for (int i = 0; i < _blocks.Length; i++)
                for (int j = i + 1; j < _blocks.Length; j++)
                    if (_blocks[j].transform.position == _blocks[i].transform.position)
                        LocalDestroy(_blocks[j]);

            void LocalDestroy(Block block)
            {
                DestroyImmediate(block.gameObject);
                SetupBlocks();
                DestroyBlocksInBlock();
            }
        }
#endif
    }
}
