using UnityEngine;

namespace Agava.Playground3D.Items
{
    public interface IBlock : IItem
    {
        int Health { get; }
        GameObject BlockTemplate { get; }
    }
}