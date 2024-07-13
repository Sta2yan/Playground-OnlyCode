using UnityEngine;

namespace Agava.Blocks
{
    public class BoxGridAvailableArea : MonoBehaviour, IGridAvailableArea
    {
        [SerializeField] private Vector3Int _availableArea;

        private Vector3 _min;
        private Vector3 _max;
        private bool _calculated;

        public bool Has(Vector3Int position)
        {
            if (_calculated == false)
            {
                var center = transform.position;
                var halfSize = (Vector3)_availableArea / 2f;

                _min = center - halfSize;
                _max = center + halfSize;
                
                _calculated = true;
            }

            return position.x >= _min.x && position.x <= _max.x &&
                   position.y >= _min.y && position.y <= _max.y &&
                   position.z >= _min.z && position.z <= _max.z;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _availableArea);
        }
#endif
    }
}