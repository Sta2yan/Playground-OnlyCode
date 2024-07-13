using Agava.AdditionalPredefinedMethods;
using Agava.Blocks;
using Agava.Inventory;
using Agava.Playground3D.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Playground3D.Sandbox.Interface
{
    public class SandboxInterfaceRouter : IGameLoop
    {
        private readonly OutlineBlockView _outlineBlockView;
        private readonly GameObject _dropButton;
        private readonly ItemsList _itemsList;
        private readonly IInventory _inventory;
        private readonly BlockRotation _blockRotation;
        private readonly bool _mobileInput;

        public SandboxInterfaceRouter(OutlineBlockView outlineBlockView,
            GameObject dropButton, ItemsList itemsList, IInventory inventory,
            BlockRotation blockRotation, bool mobileInput)
        {
            _outlineBlockView = outlineBlockView;
            _dropButton = dropButton;
            _itemsList = itemsList;
            _inventory = inventory;
            _blockRotation = blockRotation;
            _mobileInput = mobileInput;
        }

        public void Update(float _)
        {
            if (EventSystem.current.IsPointerOverGameObject() && (_mobileInput == false))
            {
                _outlineBlockView.DisableOnUI();
            }
            else
            {
                _outlineBlockView.EnableOnUI();
            }

            if (_itemsList.TryGetItemById(_inventory.ItemIdBy(_inventory.SelectedSlotIndex), out var currentItem))
            {
                _dropButton.SetActive(_inventory.HasItemsWith(currentItem.Id, 1));
                //_blockRotation.gameObject.SetActive(currentItem.TryConvertTo(out IBlock block) && block.BlockTemplate != null);
            }
        }
    }
}
