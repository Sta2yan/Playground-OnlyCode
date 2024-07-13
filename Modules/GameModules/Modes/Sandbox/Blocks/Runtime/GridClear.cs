using UnityEngine;
using Agava.Blocks;

namespace Agava.Playground3D.Sandbox.Blocks
{
    public class GridClear : MonoBehaviour
    {
        private BlocksCommunication _blocksCommunication;

        public void Initialize(BlocksCommunication blocksCommunication)
        {
            _blocksCommunication = blocksCommunication;
        }

        public void ClearGrid()
        {
            if (_blocksCommunication == null)
                return;

            _blocksCommunication.ClearGrid();
        }
    }
}
