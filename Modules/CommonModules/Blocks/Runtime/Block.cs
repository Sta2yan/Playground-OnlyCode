using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField, Min(1)] private Vector3Int _size;
        [SerializeField] private BlockItem _item;
#if UNITY_EDITOR
        [Header("Editor")]
        [SerializeField] private bool _solidGizmos;
#endif

        public Vector3Int Size => Vector3Int.RoundToInt(transform.rotation * _size);
        public string ItemId => _item == null ? "" : _item.Id;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 rotatedSize = Size;
            Vector3 center = transform.position + rotatedSize / 2f;
            
            Gizmos.color = Color.blue;
            
            if (_solidGizmos)
                Gizmos.DrawCube(center, rotatedSize);
            else
                Gizmos.DrawWireCube(center, rotatedSize);
        }
#endif
    }
}
