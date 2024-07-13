using Agava.DroppedItems;
using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Playground3D.Input
{
    internal class DroppedItemCommunication : IDroppedItemCommunication
    {
        private readonly DroppedItemFactory _droppedItemsFromPosition;
        private readonly DroppedItemFactory _droppedItemsFromUser;
        private readonly Transform _root;

        public DroppedItemCommunication(Transform root, DroppedItemFactory droppedItemsFromUser, DroppedItemFactory droppedItemsFromPosition)
        {
            _droppedItemsFromPosition = droppedItemsFromPosition;
            _droppedItemsFromUser = droppedItemsFromUser;
            _root = root;
        }

        public void DropFromUser(IItem item)
        {
            _droppedItemsFromUser.Create(item.Id, item.Mesh, item.Material, _root.forward);
        }

        public void DropFromPosition(IItem item, Vector3 position, Vector3 direction)
        {
            _droppedItemsFromPosition.transform.position = position;
            _droppedItemsFromPosition.Create(item.Id, item.Mesh, item.Material, direction);
        }
    }
}
