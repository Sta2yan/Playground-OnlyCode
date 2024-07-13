using UnityEngine;

namespace Agava.Playground3D.Items
{
    public interface IBotSpawn : IItem
    {
        public GameObject BotTemplate { get; }
    }
}
