using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Blocks
{
    public class BlockGrid
    {
        private const int GridUnit = 1;

        private readonly Dictionary<Vector3Int, GridBlock> _grid = new();
        private readonly IGridAvailableArea _gridAvailableArea;

        public BlockGrid(IGridAvailableArea gridAvailableArea)
        {
            _gridAvailableArea = gridAvailableArea;
        }

        internal Dictionary<Vector3Int, GridBlock> Grid => new(_grid);

        public bool CanAdd(Vector3Int position, Vector3Int size, bool withRotation = false)
        {
            if (_gridAvailableArea.Has(position) == false)
                return false;

            var gridPositions = position.ToGridPositionWith(size, withRotation);

            foreach (var gridPosition in gridPositions)
                if (HasBlockIn(gridPosition))
                    return false;

            return true;
        }

        public void Add(string blockId, int health, Vector3Int position, Vector3Int blockSize, GameObject block = null, bool withRotation = false, GameObject interactableObject = null, bool triggerCollider = false, bool setupBlock = false)
        {
            if (CanAdd(position, blockSize, withRotation) == false)
                throw new InvalidOperationException();

            Vector3Int[] gridPositions = position.ToGridPositionWith(blockSize, withRotation);

            var gridBlock = new GridBlock(blockId, health, blockSize, block, gridPositions, interactableObject, triggerCollider, setupBlock);

            foreach (var positions in gridPositions)
                _grid.Add(positions, gridBlock);
        }

        public void ApplyDamageTo(Vector3Int position, int damage)
        {
            if (HasBlockIn(position) == false)
                throw new InvalidOperationException();

            var block = _grid[position];

            block.ApplyDamage(damage);

            if (block.Broken == false)
                return;

            foreach (var gridPosition in block.OccupiedPositions)
                _grid.Remove(gridPosition);
        }

        public bool HasBlocksNearby(Vector3Int position)
        {
            var gridEnumerator = _grid.GetEnumerator();

            while (gridEnumerator.MoveNext())
                if (FindNearbyBlock())
                    return true;

            return false;

            bool FindNearbyBlock() =>
                Vector3Int.Distance(gridEnumerator.Current.Key, position) <= GridUnit;
        }

        public bool HasBlockIn(Vector3Int position)
        {
            return _grid.ContainsKey(position);
        }

        public void ClearGrid(bool removeStartUpBlocks)
        {
            var gridCopy = new Dictionary<Vector3Int, GridBlock>(_grid);

            foreach (var gridBlockPosition in gridCopy)
            {
                if (HasBlockIn(gridBlockPosition.Key))
                {
                    if ((gridBlockPosition.Value.SetupBlock && (removeStartUpBlocks == false)) == false)
                    {
                        ApplyDamageTo(gridBlockPosition.Key, gridBlockPosition.Value.CurrentHealth);
                    }
                }
            }
        }

        internal GridBlock BlockBy(Vector3Int position)
        {
            if (HasBlockIn(position) == false)
                throw new InvalidOperationException();

            return _grid[position];
        }

        internal void Visualize(GridView view)
        {
            view.Render(_grid);
        }
    }
}
