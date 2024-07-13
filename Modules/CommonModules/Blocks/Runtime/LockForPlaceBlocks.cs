using UnityEngine;

namespace Agava.Blocks
{
    public class LockForPlaceBlocks : MonoBehaviour
    {
        [SerializeField, Min(1)] private Vector3Int _size = Vector3Int.one;

        public Vector3Int Size => _size;
        
        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + new Vector3(0, _size.y / 2, 0), _size);
        }
    }
}
