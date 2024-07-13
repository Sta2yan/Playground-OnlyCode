using Agava.Blocks;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEditor;

namespace Agava.Editor
{
    public class CreateGridSetup : MonoBehaviour
    {
        [SerializeField] private GameObject _blockPrefab;
        [SerializeField] private Vector3Int _setupSize;
        [SerializeField] private Transform _center;

#if UNITY_EDITOR
        [ProButton]
        public void Create()
        {
            Vector3 startPosition = transform.position;
            //startPosition.x = startPosition.x - _center.position.x / 2.0f;

            for (int xRow = 0; xRow < _setupSize.x; xRow++)
            {
                for (int yRow = 0; yRow < _setupSize.y; yRow++)
                {
                    for (int zRow = 0; zRow < _setupSize.z; zRow++)
                    {
                        GameObject block = PrefabUtility.InstantiatePrefab(_blockPrefab, transform) as GameObject;
                        block.transform.position = startPosition + new Vector3(xRow, yRow, zRow);
                    }
                }
            }
        }
#endif

#if UNITY_EDITOR
        [ProButton]
        public void Clear()
        {
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
        }
#endif

    }
}
