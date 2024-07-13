using System.Linq;
using Agava.Blocks;
using UnityEngine;

namespace Agava.Playground3D.Input
{
    internal class BlockRules : IBlockRules
    {
        private readonly Transform _character;
        private readonly LockForPlaceBlocks _lockForPlaceBlocks;
        private readonly Transform _characterPosition;
        private readonly BlockGrid _grid;
        private readonly float _blockPlaceDistance;

        public float PlaceDistance => _blockPlaceDistance;
        
        public BlockRules(Transform characterPosition, BlockGrid grid, LockForPlaceBlocks lockForPlaceBlocks, float blockPlaceDistance)
        {
            _characterPosition = characterPosition;
            _blockPlaceDistance = blockPlaceDistance;
            _lockForPlaceBlocks = lockForPlaceBlocks;
            _grid = grid;
        }

        public bool CanPlace(Vector3 positionHit, Vector3 scale)
        {
            var positionOnGrid = positionHit.ConvertToGrid();

            if (Vector3.Distance(_characterPosition.position, positionOnGrid) > _blockPlaceDistance)
                return false;

            var positions = _lockForPlaceBlocks.transform.position.ToGridPositionWith(_lockForPlaceBlocks.Size);

            return positions.All(userPosition => userPosition != positionOnGrid) && _grid.HasBlocksNearby(positionOnGrid) && _grid.CanAdd(positionOnGrid, Vector3Int.FloorToInt(scale));
        }

        public bool CanRemove(Vector3 blockPosition)
        {
            return Vector3.Distance(_characterPosition.position, blockPosition) <= _blockPlaceDistance;
        }
    }
}
