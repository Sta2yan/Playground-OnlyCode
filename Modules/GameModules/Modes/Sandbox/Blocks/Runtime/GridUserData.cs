using UnityEngine;
using System.Collections.Generic;
using Agava.Save;
using Agava.Blocks;
using System;
using Agava.Utils;
using Agava.Playground3D.Items;

namespace Agava.Playground3D.Sandbox.Blocks
{
    public class GridUserData : MonoBehaviour
    {
        [SerializeField] private string _saveKey;

        public bool Available { get; private set; } = true;

        private BlocksCommunication _blocksCommunication;
        private ItemsList _itemsList;

        public void Initialize(BlocksCommunication blocksCommunication, ItemsList itemsList)
        {
            _blocksCommunication = blocksCommunication;
            _itemsList = itemsList;
        }

        public void Save()
        {
            Available = false;
            GridDataObject dataObject = new GridDataObject(_blocksCommunication.SaveGrid());
            string jsonData = JsonUtility.ToJson(dataObject);
            jsonData = jsonData.Replace(@"""", "&quot;");
            SaveFacade.SetString(_saveKey, jsonData);
            Available = true;
        }

        public void Load()
        {
            if (SaveFacade.HasKey(_saveKey) == false)
                return;

            string jsonData = SaveFacade.GetString(_saveKey, null);

            if (jsonData == null)
                return;

            jsonData = jsonData.Replace("&quot;", @"""");
            GridDataObject dataObject = JsonUtility.FromJson<GridDataObject>(jsonData);

            Vector3Int position;
            string blockId;

            for (int i = 0; i < dataObject.Entries; i++)
            {
                position = dataObject.Keys[i].Vector3Int;
                blockId = dataObject.Values[i];

                if (_itemsList.TryGetItemById(blockId, out IItem item) == false)
                    return;

                if (item.TryConvertTo(out IBlock blockItem) == false)
                    return;

                GameObject interactableObject = null;
                bool triggerCollider = false;

                if (blockItem.TryConvertTo(out IInteractableBlock interactableBlockItem))
                {
                    interactableObject = interactableBlockItem.InteractableObject;
                    triggerCollider = interactableBlockItem.TriggerCollider;
                }

                if (blockItem.BlockTemplate.TryGetComponent(out Block blockTemplate) == false)
                    return;

                _blocksCommunication.TryPlace(blockTemplate,
                    position,
                    blockItem.Id,
                    blockItem.Health,
                    false, null,
                    interactableObject: interactableObject,
                    triggerCollider: triggerCollider,
                    setupBlock: false);
            }
        }

        [Serializable]
        private class GridDataObject
        {
            public SerializableVector3Int[] Keys;
            public string[] Values;
            public int Entries;

            public GridDataObject(Dictionary<Vector3Int, string> grid)
            {
                List<SerializableVector3Int> keys = new();
                List<string> values = new();

                foreach (var pair in grid)
                {
                    keys.Add(new SerializableVector3Int(pair.Key));
                    values.Add(pair.Value);
                }

                Keys = keys.ToArray();
                Values = values.ToArray();
                Entries = grid.Count;
            }
        }
    }
}
