using UnityEngine;

namespace Agava.Blocks.Sample
{
    public class InputTest : MonoBehaviour
    {
        [SerializeField] private Block _template;
        [SerializeField] private OutlineBlockView _outline;
        
        private BlocksCommunication _blocksCommunication;
        private BlockGrid _grid;
        
        private void Awake()
        {
            _grid = new BlockGrid(null);
            _blocksCommunication = new BlocksCommunication(_grid, null, null);
            var allBlocks = FindObjectsOfType<Block>();

            foreach (var block in allBlocks)
                _grid.Add("", 1, block.transform.position.ConvertToGrid(), block.Size, null);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 5f))
                {
                    if (_grid.CanAdd(hit.point.ConvertToGrid(), _template.Size))
                        _blocksCommunication.TryPlace(_template, hit.point, "", 1);
                }
            }
            
            if (Physics.Raycast(ray, out hit, 5f))
            {
                if (_grid.CanAdd(hit.point.ConvertToGrid(), _template.Size))
                    _outline.TryRender(hit.point, _template.Size);
            }
            else
            {
                _outline.Disable();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (Physics.Raycast(ray, out hit, 5f))
                {
                    if (hit.collider.TryGetComponent(out Block block))
                        _blocksCommunication.ApplyDamage(block.transform.position, 1);
                }
            }
        }
    }
}
