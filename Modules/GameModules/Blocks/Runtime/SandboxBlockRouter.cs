using System;
using Agava.Blocks;
using Agava.AdditionalPredefinedMethods;
using Agava.Input;
using Agava.Inventory;
using Agava.Movement;
using Agava.Playground3D.Items;
using UnityEngine;
using Random = UnityEngine.Random;
using Agava.Playground3D.Input;
using Agava.Audio;
using Agava.ExperienceSystem;
using Agava.Playground3D.PathFinding;

namespace Agava.Playground3D.Blocks
{
    public class SandboxBlockRouter : IGameLoop, IBlockRouter
    {
        private const float PlaceBlockSearchDistance = 100f;
        private const int InventoryBlockCount = 1;
        private const int PickDamage = 999999;

        private readonly IInput _input;
        private readonly IBlockRules _rules;
        private readonly BlocksCommunication _blocksCommunication;
        private readonly IBlockAnimation _blockAnimation;
        private readonly ICameraMovement _cameraMovement;
        private readonly IInventory _inventory;
        private readonly OutlineBlockView _outlineView;
        private readonly ItemsList _itemsList;
        private readonly LayerMask _layerMask;
        private readonly Hand _hand;
        private readonly ISoundSource _blockPlaceSoundSource;
        private readonly IBlockDamageSource _blockDamageSource;
        private readonly PathFindingUpdate _pathFindingUpdate;

        private readonly ExperienceEventsContainer _experienceEventsContainer;
        private readonly IItemExperienceEventRule _placeBlockEventRule;
        private readonly IItemExperienceEventRule _destroyBlockEventRule;

        private readonly Transform _blockPlacePoint;
        private readonly Transform _cameraPivot;

        private float _currentPickDelay;

        public SandboxBlockRouter(IInput input, IBlockRules rules, BlocksCommunication blocksCommunication,
               ICameraMovement cameraMovement, IInventory inventory,
               ItemsList itemsList, LayerMask layerMask, OutlineBlockView outlineView, Transform blockPlacePoint,
               Transform cameraPivot, Hand hand, IBlockAnimation blockAnimation, ISoundSource blockPlaceSoundSource,
               IBlockDamageSource blockDamageSource, ExperienceEventsContainer experienceEventsContainer,
               IItemExperienceEventRule placeBlockEventRule, IItemExperienceEventRule destroyBlockEventRule,
               PathFindingUpdate pathFindingUpdate)
        {
            _input = input;
            _rules = rules;
            _blocksCommunication = blocksCommunication;
            _blockPlacePoint = blockPlacePoint;
            _blockAnimation = blockAnimation;
            _cameraMovement = cameraMovement;
            _cameraPivot = cameraPivot;
            _outlineView = outlineView;
            _itemsList = itemsList;
            _inventory = inventory;
            _layerMask = layerMask;
            _hand = hand;
            _blockPlaceSoundSource = blockPlaceSoundSource;
            _blockDamageSource = blockDamageSource;
            _experienceEventsContainer = experienceEventsContainer;
            _placeBlockEventRule = placeBlockEventRule;
            _destroyBlockEventRule = destroyBlockEventRule;
            _pathFindingUpdate = pathFindingUpdate;
        }

        public void Update(float _)
        {
            if (_outlineView.DisabledOnUI)
            {
                _outlineView.Disable();
                return;
            }

            UpdatePickDigDelay();

            if (TryRenderOutlineWithRay() == false)
                RenderOutline();

            if (TryGetBlock(out IBlock blockItem, out Block blockTemplate))
            {
                if (_input.PlaceBlock())
                {
                    if (TryPlaceBlock(blockItem, blockTemplate, out Vector3 placePosition) || TryPlaceBlockNear(blockItem, blockTemplate, out placePosition))
                    {
                        _pathFindingUpdate.RequestUpdateAtPosition(placePosition);

                        if (_placeBlockEventRule.TryGetExperienceEvent(blockItem, out ExperienceEvent placeBlockEvent))
                            _experienceEventsContainer.TriggerEvent(placeBlockEvent);

                        _blockAnimation.Place();
                        _blockPlaceSoundSource.Play();
                    }
                }
            }

            if (_hand.CurrentItem.TryConvertTo(out IPick pick))
            {
                if (_input.RemoveBlock())
                {
                    if (_currentPickDelay > 0)
                        return;

                    _currentPickDelay = pick.DigDelay;
                    _blockAnimation.Place();

                    if (TryRemoveBlock(out Vector3 removePosition, out string id, PickDamage))
                    {
                        _pathFindingUpdate.RequestUpdateAtPosition(removePosition);

                        if (_itemsList.TryGetItemById(id, out IItem item))
                        {
                            if (_destroyBlockEventRule.TryGetExperienceEvent(item, out ExperienceEvent destroyBlockEvent))
                                _experienceEventsContainer.TriggerEvent(destroyBlockEvent);
                        }
                    }
                }
            }
        }

        private void UpdatePickDigDelay()
        {
            if (_currentPickDelay < 0)
            {
                _currentPickDelay = 0;
                return;
            }

            _currentPickDelay -= Time.deltaTime;
        }

        private bool TryPlaceBlock(IBlock blockItem, Block blockTemplate, out Vector3 placePosition)
        {
            placePosition = Vector3.zero;

            if (PositionPlace(out var positionHit, out var positionNormal) == false)
                return false;

            GameObject interactableObject = null;
            bool triggerCollider = false;

            if (blockItem.TryConvertTo(out IInteractableBlock interactableBlockItem))
            {
                interactableObject = interactableBlockItem.InteractableObject;
                triggerCollider = interactableBlockItem.TriggerCollider;
            }

            placePosition = positionHit;

            if (_rules.CanPlace(placePosition, blockTemplate.Size))
                if (_blocksCommunication.TryPlace(blockTemplate, placePosition, blockItem.Id, blockItem.Health, interactableObject: interactableObject, triggerCollider: triggerCollider))
                    return true;

            placePosition = positionHit + positionNormal;

            if (_rules.CanPlace(placePosition, blockTemplate.Size))
                if (_blocksCommunication.TryPlace(blockTemplate, placePosition, blockItem.Id, blockItem.Health, interactableObject: interactableObject, triggerCollider: triggerCollider))
                    return true;

            return false;
        }

        private bool TryPlaceBlockNear(IBlock blockItem, Block blockTemplate, out Vector3 placePosition)
        {
            GameObject interactableObject = null;
            bool triggerCollider = false;

            if (blockItem.TryConvertTo(out IInteractableBlock interactableBlockItem))
            {
                interactableObject = interactableBlockItem.InteractableObject;
                triggerCollider = interactableBlockItem.TriggerCollider;
            }

            placePosition = Vector3.zero;

            for (var i = 1; i <= _rules.PlaceDistance; i++)
            {
                placePosition = NearPositionPlace(i);

                if (_blocksCommunication.TryPlaceNear(blockTemplate, placePosition, blockItem.Id, blockItem.Health, interactableObject: interactableObject, triggerCollider: triggerCollider))
                    return true;
            }

            return false;
        }

        private bool TryGetBlock(out IBlock item, out Block block)
        {
            item = default;
            block = default;

            if (TryGetBlockItem(out var blockItem) == false)
                return false;

            if (blockItem.BlockTemplate.TryGetComponent(out Block blockTemplate) == false)
                return false;

            item = blockItem;
            block = blockTemplate;
            return true;
        }

        private bool TryRemoveBlock(out Vector3 removePosition, out string id, int damage)
        {
            removePosition = Vector3.zero;
            id = "";

            if (TryFindBlockPosition(out Vector3 blockPosition) == false)
                return false;

            if (_blocksCommunication.HasBlockIn(blockPosition) == false)
                return false;

            if (_rules.CanRemove(blockPosition) == false)
                return false;

            removePosition = blockPosition;
            id = _blocksCommunication.BlockIdIn(blockPosition);
            _blocksCommunication.ApplyDamage(removePosition, damage, _blockDamageSource);

            return _blocksCommunication.HasBlockIn(removePosition) == false;
        }

        private bool TryRenderOutlineWithRay()
        {
            if (TryGetBlockItem(out var blockItem) == false)
            {
                _outlineView.Disable();
                return false;
            }

            if (blockItem.BlockTemplate.TryGetComponent(out Block blockTemplate) == false)
                return false;

            if (PositionPlace(out var positionHit, out var positionNormal) == false)
            {
                _outlineView.Disable();
                return false;
            }

            if (_rules.CanPlace(positionHit, _outlineView.transform.localScale))
                if (_outlineView.TryRender(positionHit, blockTemplate.Size))
                    return true;

            if (_rules.CanPlace(positionHit + positionNormal, _outlineView.transform.localScale))
                if (_outlineView.TryRender(positionHit + positionNormal, blockTemplate.Size))
                    return true;

            _outlineView.Disable();
            return false;
        }

        private void RenderOutline()
        {
            if (TryGetBlockItem(out var blockItem) == false)
            {
                _outlineView.Disable();
                return;
            }

            if (blockItem.BlockTemplate.TryGetComponent(out Block blockTemplate) == false)
                return;

            for (var i = 1; i <= _rules.PlaceDistance; i++)
                if (_outlineView.TryRenderNear(NearPositionPlace(i), blockTemplate.Size))
                    return;
        }

        private bool TryGetBlockItem(out IBlock block)
        {
            block = null;

            if (_inventory.CanRemove(_inventory.SelectedSlotIndex, InventoryBlockCount) == false)
                return false;

            if (_itemsList.TryGetItemById(_inventory.ItemIdBy(_inventory.SelectedSlotIndex), out var item) == false)
                return false;

            if (item.TryConvertTo(out IBlock blockItem) == false)
                return false;

            block = blockItem;
            return true;
        }

        private bool TryFindBlockPosition(out Vector3 blockPosition)
        {
            blockPosition = Vector3.zero;
            var cameraRay = RayByCameraPerson();

            if (Physics.Raycast(cameraRay, out var blockHit, PlaceBlockSearchDistance, _layerMask) == false)
                return false;

            if (blockHit.collider.TryGetComponent(out Block _) == false)
                return false;

            blockPosition = blockHit.point - blockHit.normal * 0.5f;
            return true;
        }

        private bool PositionPlace(out Vector3 position, out Vector3 normal)
        {
            position = Vector3.zero;
            normal = Vector3.zero;

            var cameraRay = RayByCameraPerson();

            if (Physics.Raycast(cameraRay, out var hitInfo, PlaceBlockSearchDistance, _layerMask) == false)
                return false;

            if (hitInfo.collider == null)
                return false;

            if (hitInfo.collider.isTrigger)
                return false;

            position = hitInfo.point;
            normal = hitInfo.normal;
            return true;
        }

        private Vector3 NearPositionPlace(int multiply = 1)
        {
            var position = _blockPlacePoint.transform.position;
            var screenPoint = _cameraMovement.CameraMain.WorldToScreenPoint(position);
            screenPoint.z = 0;
            var direction = UnityEngine.Input.mousePosition - screenPoint;
            direction.z = direction.y;
            direction.y = 0;

            return position + Quaternion.Euler(0, _cameraPivot.eulerAngles.y, 0f) * direction.normalized * multiply;
        }

        private Ray RayByCameraPerson()
        {
            return _cameraMovement.FirstPersonPerspective ?
                   _cameraMovement.CameraMain.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f)) :
                   _cameraMovement.CameraMain.ScreenPointToRay(UnityEngine.Input.mousePosition);
        }
    }
}
