using System.Collections.Generic;
using System.Linq;
using Agava.AdditionalPredefinedMethods;
using Agava.Playground3D.Items;
using UnityEngine;
using UnityEngine.Rendering;

namespace Agava.Blocks
{
    public class GridView : IGameLoop
    {
        private readonly ItemsList _itemsList;
        private readonly IBlockDrawRule _blockDrawRule;
        private readonly Dictionary<string, List<Matrix4x4>> _blockMatrices = new();
        private readonly Dictionary<Vector3Int, Collider> _colliders = new();
        private readonly List<Vector3Int> _lastRenderedPositions = new();
        private readonly IItem _nullableItem = new NullableItem();

        private GameObject _collidersObject;

        public GridView(ItemsList itemsList, IBlockDrawRule blockDrawRule)
        {
            _itemsList = itemsList;
            _blockDrawRule = blockDrawRule;
        }

        public void Update(float deltaTime)
        {
            foreach (var pair in _blockMatrices)
            {
                if (pair.Key == _nullableItem.Id)
                    continue;

                if (_itemsList.TryGetItemById(pair.Key, out IItem item) == false)
                    continue;

                if (pair.Value.Count == 0)
                    continue;

                #if UNITY_EDITOR || (ANDROID_BUILD && !UNITY_EDITOR)
                RenderParams renderParams = new RenderParams(item.Material)
                {
                    shadowCastingMode = ShadowCastingMode.On,
                    receiveShadows = true,
                };      
                #elif YANDEX_GAMES && !UNITY_EDITOR
                RenderParams renderParams = new RenderParams(item.Material);
                #endif

                Graphics.RenderMeshInstanced(renderParams, item.Mesh, 0, pair.Value);
            }
        }

        internal void Render(IReadOnlyDictionary<Vector3Int, GridBlock> grid)
        {
            _collidersObject ??= new GameObject("Colliders");

            RemoveExtraPositions(grid);
            AddNewPositions(grid);
        }

        private void RemoveExtraPositions(IReadOnlyDictionary<Vector3Int, GridBlock> grid)
        {
            for (int i = _lastRenderedPositions.Count - 1; i >= 0; i--)
            {
                var position = _lastRenderedPositions[i];

                if (grid.ContainsKey(position))
                    continue;

                _lastRenderedPositions.Remove(position);

                if (_colliders.ContainsKey(position))
                {
                    Object.Destroy(_colliders[position].gameObject);
                    _colliders.Remove(position);
                }

                var matrix = MatrixBy(ToWorldPosition(position));

                foreach (var pair in _blockMatrices)
                {
                    if (pair.Value.Any(matrix.Equals) == false)
                        continue;

                    pair.Value.Remove(matrix);
                }
            }
        }

        private void AddNewPositions(IReadOnlyDictionary<Vector3Int, GridBlock> grid)
        {
            foreach (var pair in grid)
            {
                var position = pair.Key;
                var gridBlock = pair.Value;
                string id = gridBlock.ItemId;

                if (id == _nullableItem.Id)
                    continue;

                if (_blockDrawRule.Need(id) == false)
                    continue;

                if (_lastRenderedPositions.Contains(position))
                    continue;

                _lastRenderedPositions.Add(position);

                Vector3 targetPosition = ToWorldPosition(position);

                var matrix = MatrixBy(targetPosition);

                var newBlock = new GameObject("Colliders", typeof(BoxCollider), typeof(Block))
                {
                    transform =
                    {
                        parent = _collidersObject.transform
                    }
                };
                
                var collider = newBlock.GetComponent<BoxCollider>();
                collider.center = targetPosition;
                collider.size = Vector3.one;
                collider.isTrigger = gridBlock.TriggerCollider;

                if (gridBlock.InteractableObject != null)
                {
                    Object.Instantiate(gridBlock.InteractableObject, collider.center, Quaternion.identity, newBlock.transform);
                }

                _colliders.Add(position, collider);

                if (_blockMatrices.ContainsKey(id))
                {
                    _blockMatrices[id].Add(matrix);
                }
                else
                {
                    _blockMatrices.Add(id, new List<Matrix4x4> { matrix });
                }
            }
        }

        private Matrix4x4 MatrixBy(Vector3 worldPosition)
        {
            return Matrix4x4.TRS(worldPosition, Quaternion.identity, Vector3.one);
        }

        private Vector3 ToWorldPosition(Vector3Int position)
        {
            return position + Vector3.one * 0.5f;
        }
    }
}