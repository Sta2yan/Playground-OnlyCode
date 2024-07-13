using Lean.Localization;
using UnityEngine;

namespace Agava.Playground3D.Items
{
    public abstract class Item : ScriptableObject, IItem
    {
        [field: Header("Common Information")]
        [field: SerializeField] public string Id { get; private set; }

        [field: SerializeField, LeanTranslationName] public string TranslationId { get; private set; }
        [field: SerializeField] public int MaxStack { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject HandTemplate { get; private set; }
        [field: SerializeField] public Mesh Mesh { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public bool CanDrop { get; private set; } = true;
        [field: SerializeField] public bool DebugItem { get; private set; } = false;
    }
}