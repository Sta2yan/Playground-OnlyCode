using UnityEngine;
using Agava.DroppedItems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agava.Playground3D.Items;
using TMPro;

namespace Agava.ResourceGenerator
{
    public class ResourceGenerator : MonoBehaviour
    {
        private readonly List<DroppedItem> _droppedItems = new();
        
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private int _capacity = 32;
        [SerializeField] private Item _spawnItem;
        [SerializeField] private DroppedItemFactory _droppedItemFactory;
        [SerializeField] private TMP_Text _delayText;

        private void Awake()
        {
            StartCoroutine(SpawningItems(_delayBetweenSpawn));
        }

        private IEnumerator SpawningItems(float delay)
        {
            while (true)
            {
                yield return new WaitUntil(() => _droppedItems.Count(item => item != null) < _capacity);
                _droppedItems.RemoveAll(item => item == null);
                
                float elapsedTime = 0f;

                while (elapsedTime < delay)
                {
                    elapsedTime += Time.deltaTime;
                    
                    if (_delayText != null)
                        _delayText.text = $"{(delay - elapsedTime):00}";
                    
                    yield return null;
                }
            
                var instance = _droppedItemFactory.Create(_spawnItem.Id, _spawnItem.Mesh, _spawnItem.Material, Vector3.down);
                _droppedItems.Add(instance);
            }
        }
    }
}
