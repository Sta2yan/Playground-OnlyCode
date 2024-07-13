using System;
using System.Collections.Generic;
using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Blocks
{
    public class BlocksCommunication
    {
        private readonly BlockGrid _blockGrid;
        private readonly GridView _gridView;
        private readonly IBlockHealthView _blockHealthView;
        private readonly IItem _nullableItem = new NullableItem();
        private readonly bool _canRemoveStartUpBlocks;

        public BlocksCommunication(BlockGrid blockGrid, GridView gridView, IBlockHealthView blockHealthView, bool canRemoveStartUpBlocks = true)
        {
            _blockGrid = blockGrid;
            _gridView = gridView;
            _blockHealthView = blockHealthView;
            _canRemoveStartUpBlocks = canRemoveStartUpBlocks;
        }

        public Dictionary<Vector3Int, string> SaveGrid()
        {
            Dictionary<Vector3Int, string> loadedGrid = new();

            var grid = _blockGrid.Grid;
            GridBlock gridBlock;

            foreach (var blockPosition in grid)
            {
                gridBlock = blockPosition.Value;

                if (gridBlock.SetupBlock == false)
                {
                    loadedGrid.Add(blockPosition.Key, gridBlock.ItemId);
                }
            }

            return loadedGrid;
        }

        public void ClearGrid()
        {
            _blockGrid.ClearGrid(_canRemoveStartUpBlocks);
            _blockGrid.Visualize(_gridView);
        }

        public bool TryPlace(Block block, Vector3 position, string id, int health, bool withRotation = false, GameObject blockGameObject = null, GameObject interactableObject = null, bool triggerCollider = false, bool setupBlock = false)
        {
            var gridPosition = position.ConvertToGrid(withRotation);

            if (_blockGrid.CanAdd(gridPosition, block.Size) == false)
                return false;

            _blockGrid.Add(id, health, gridPosition, block.Size, blockGameObject, withRotation, interactableObject, triggerCollider, setupBlock);

            if (id != _nullableItem.Id)
                _blockGrid.Visualize(_gridView);

            return true;
        }

        public bool TryPlaceNear(Block block, Vector3 position, string id, int health, bool withRotation = false, GameObject blockGameObject = null, GameObject interactableObject = null, bool triggerCollider = false, bool setupBlock = false)
        {
            var positionOnGrid = position.ConvertToGrid();

            return _blockGrid.HasBlocksNearby(positionOnGrid) && TryPlace(block, position, id, health, withRotation, blockGameObject, interactableObject, triggerCollider, setupBlock);
        }

        public void ApplyDamage(Vector3 worldPosition, int damage, IBlockDamageSource blockDamageSource = null)
        {
            var gridPosition = worldPosition.ConvertToGrid();

            if (_blockGrid.HasBlockIn(gridPosition) == false)
                throw new InvalidOperationException();

            if (_blockGrid.BlockBy(gridPosition).ItemId == _nullableItem.Id)
                return;

            var block = _blockGrid.BlockBy(gridPosition);

            if (block.SetupBlock && (_canRemoveStartUpBlocks == false))
                return;

            _blockGrid.ApplyDamageTo(gridPosition, damage);
            _blockHealthView.Render(block.MaxHealth, block.CurrentHealth, gridPosition, block.Size, blockDamageSource);

            if (_blockGrid.HasBlockIn(gridPosition))
            {
                if (block.Broken)
                    _blockHealthView.Disable();
            }
            else
            {
                _blockGrid.Visualize(_gridView);
                _blockHealthView.Disable();
            }
        }

        public bool HasBlockIn(Vector3 worldPosition)
        {
            return _blockGrid.HasBlockIn(worldPosition.ConvertToGrid());
        }

        public string BlockIdIn(Vector3 worldPosition)
        {
            var blockGridPosition = worldPosition.ConvertToGrid();

            if (_blockGrid.HasBlockIn(blockGridPosition) == false)
                throw new InvalidOperationException();

            return _blockGrid.BlockBy(blockGridPosition).ItemId;
        }
    }
}
