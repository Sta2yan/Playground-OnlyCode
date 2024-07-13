using UnityEngine;

namespace Agava.Playground3D.Items
{
    public interface IItem
    {
        string Id { get; }
        string TranslationId { get; }
        int MaxStack { get; }
        Sprite Icon { get; }
        GameObject HandTemplate { get; }
        Mesh Mesh { get; }
        Material Material { get; }
        bool CanDrop { get; }
        bool DebugItem { get; }
    }
}