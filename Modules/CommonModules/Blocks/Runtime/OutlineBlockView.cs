using UnityEngine;

namespace Agava.Blocks
{
    public class OutlineBlockView : MonoBehaviour
    {
        private BlockGrid _grid;

        public bool DisabledOnUI { get; private set; } = false;

        private void Awake()
        {
            Disable();
        }

        public void Init(BlockGrid grid)
        {
            _grid = grid;
        }

        public bool TryRender(Vector3 position, Vector3Int size)
        {
            var gridPosition = position.ConvertToGrid();

            if (_grid.CanAdd(gridPosition, size) == false)
            {
                Disable();
                return false;
            }

            transform.localScale = size;
            transform.position = gridPosition;
            gameObject.SetActive(true);
            return true;
        }

        public bool TryRenderNear(Vector3 position, Vector3Int size)
        {
            var gridPosition = position.ConvertToGrid();

            if (_grid.CanAdd(gridPosition, size) == false)
            {
                Disable();
                return false;
            }

            if (_grid.HasBlocksNearby(gridPosition) == false)
            {
                Disable();
                return false;
            }

            transform.localScale = size;
            transform.position = gridPosition;
            gameObject.SetActive(true);
            return true;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void DisableOnUI()
        {
            DisabledOnUI = true;
        }

        public void EnableOnUI()
        {
            DisabledOnUI = false;
        }
    }
}
