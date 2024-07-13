using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Playground3D.Input
{
    public interface IDroppedItemCommunication
    {
        void DropFromUser(IItem item);

        void DropFromPosition(IItem item, Vector3 position, Vector3 direction);
    }
}
