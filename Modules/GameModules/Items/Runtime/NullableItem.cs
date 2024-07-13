using UnityEngine;

namespace Agava.Playground3D.Items
{
    public class NullableItem : IItem
    {
        public string Id => "";
        public string TranslationId => "";
        public int MaxStack => 0;
        public Sprite Icon => null;
        public GameObject HandTemplate => null;
        public Mesh Mesh => null;
        public Material Material => null;
        public bool CanDrop => false;
        public bool DebugItem => false;
    }
}