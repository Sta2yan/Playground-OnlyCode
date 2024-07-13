using System.Collections.Generic;
using System.Linq;
using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Blocks
{
    [CreateAssetMenu(menuName = "Create GridBlockColliderRule", fileName = "GridBlockColliderRule", order = 56)]
    public class GridBlockDrawRule : ScriptableObject, IBlockDrawRule
    {
        [SerializeField] private List<BlockItem> _blocksWithoutDrawing;

        private HashSet<string> _blocksIds;
        
        public bool Need(string itemId)
        {
            _blocksIds ??= _blocksWithoutDrawing.Select(block => block.Id).ToHashSet();

            return _blocksIds.Contains(itemId) == false;
        }
    }
}