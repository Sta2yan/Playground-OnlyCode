using System.Collections.Generic;
using UnityEngine;

namespace Agava.Blocks
{
    public static class GridExtensions
    {
        public static Vector3Int ConvertToGrid(this Vector3 worldPosition, bool withRotation = false)
            => withRotation ? Vector3Int.RoundToInt(worldPosition) : Vector3Int.FloorToInt(worldPosition);
        
        public static Vector3Int[] ToGridPositionWith(this Vector3 startPosition, Vector3 size, bool withRotation = false)
        {
            var gridPositions = new List<Vector3Int>();
            var startGridPosition = startPosition.ConvertToGrid(withRotation);

            int startX = (int)Mathf.Min(0, size.x);
            int startY = (int)Mathf.Min(0, size.y);
            int startZ = (int)Mathf.Min(0, size.z);

            int endX = (int)Mathf.Max(0, size.x);
            int endY = (int)Mathf.Max(0, size.y);
            int endZ = (int)Mathf.Max(0, size.z);

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    for (int z = startZ; z < endZ; z++)
                    {
                        gridPositions.Add(new Vector3Int(startGridPosition.x + x, startGridPosition.y + y, startGridPosition.z + z));
                    }
                }
            }

            return gridPositions.ToArray();
        }

        public static Vector3Int[] ToGridPositionWith(this Vector3Int startPosition, Vector3Int size, bool withRotation = false)
        {
            return ((Vector3)startPosition).ToGridPositionWith(size, withRotation);
        }
    }
}
