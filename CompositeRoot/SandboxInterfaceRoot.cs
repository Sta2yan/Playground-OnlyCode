using UnityEngine;
using Agava.Blocks;
using Agava.Playground3D.Sandbox.Blocks;
using Agava.Playground3D.Items;
using Agava.Playground3D.Sandbox.Interface;
using Agava.AdditionalPredefinedMethods;
using Agava.Inventory;

namespace Agava.Playground3D.CompositeRoot
{
    public class SandboxInterfaceRoot : CompositeRoot
    {
        [SerializeField] private BlocksRoot _blocksRoot;
        [SerializeField] private GridClear _gridClear;
        [SerializeField] private GridUserData _gridUserData;
        [SerializeField] private GameObject _itemPanelButtonText;
        [SerializeField] private GameObject _openInventoryButtonText;
        [SerializeField] private GameObject[] _inventorySlotNumbers;
        [SerializeField] private OutlineBlockView _outlineBlockView;
        [SerializeField] private GameObject _dropButton;
        [SerializeField] private BlockRotation _blockRotation;

        private ItemsList _itemsList;
        private IInventory _inventory;
        private bool _mobile;

        private IGameLoop _gameLoop;

        public void Initialize(ItemsList itemsList, IInventory inventory, bool mobile)
        {
            _itemsList = itemsList;
            _inventory = inventory;
            _mobile = mobile;
        }

        public override void Compose()
        {
            BlocksCommunication blocksCommunication = _blocksRoot.BlocksCommunication;

            _gridClear.Initialize(blocksCommunication);
            _gridUserData.Initialize(blocksCommunication, _itemsList);
            _gridUserData.Load();

            _itemPanelButtonText.SetActive(!_mobile);
            _openInventoryButtonText.SetActive(!_mobile);

            foreach (var slot in _inventorySlotNumbers)
                slot.SetActive(!_mobile);

            SandboxInterfaceRouter interfaceRouter = new SandboxInterfaceRouter(_outlineBlockView, _dropButton, _itemsList, _inventory, _blockRotation, _mobile);
            _gameLoop = interfaceRouter as IGameLoop;
        }

        private void Update()
        {
            if (_gameLoop != null)
                _gameLoop.Update(Time.deltaTime);
        }
    }
}
