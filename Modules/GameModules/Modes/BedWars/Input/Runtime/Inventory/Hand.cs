using Agava.Movement;
using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Playground3D.Input
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _thirdPersonRoot;
        [SerializeField] private Transform _firstPersonRoot;

        private GameObject _itemInstance;

        public IItem CurrentItem { get; private set; } = new NullableItem();
        public GameObject ItemInstance => _itemInstance;

        private void Update()
        {
            if (ItemInstance == null)
                return;

            if (_cameraMovement == null)
                return;

            _root.position = _cameraMovement.FirstPersonPerspective ? _firstPersonRoot.position : _thirdPersonRoot.position;
            _root.rotation = _cameraMovement.FirstPersonPerspective ? _firstPersonRoot.rotation : _thirdPersonRoot.rotation;
            _root.localScale = _cameraMovement.FirstPersonPerspective ? _firstPersonRoot.localScale : _thirdPersonRoot.localScale;
            _root.SetParent(_cameraMovement.FirstPersonPerspective ? _firstPersonRoot : _thirdPersonRoot);
        }

        public void ChangeItem(IItem item)
        {
            if (CurrentItem != null && CurrentItem.Id == item.Id)
                return;

            CurrentItem = item;

            Destroy(_itemInstance);

            if (item.HandTemplate == null)
                return;

            _itemInstance = Instantiate(item.HandTemplate, _root);
        }
    }
}