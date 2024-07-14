using Agava.Audio;
using Agava.Blocks;
using Agava.ExperienceSystem;
using Agava.Input;
using Agava.Inventory;
using Agava.Movement;
using Agava.Playground3D.Blocks;
using Agava.Playground3D.PathFinding;
using Agava.Playground3D.Input;
using Agava.Playground3D.Items;
using UnityEngine;

namespace Agava.Playground3D.BlockRouterFactories
{
    public class SandboxBlockRouterFactory : BlockRouterFactory
    {
        private readonly IItemExperienceEventRule _placeBlockEventRule;
        private readonly IItemExperienceEventRule _destroyBlockEventRule;
        private readonly PathFindingUpdate _pathFindingUpdate;

        public SandboxBlockRouterFactory(IInput input,
            IInventory inventory,
            IInventoryView inventoryView,
            Hand hand, ExperienceEventsContainer experienceEventsContainer,
            IItemExperienceEventRule placeBlockEventRule,
            IItemExperienceEventRule destroyBlockEventRule, 
            PathFindingUpdate pathFindingUpdate
            )
            : base(input, inventory, inventoryView, hand, experienceEventsContainer)
        {
            _placeBlockEventRule = placeBlockEventRule;
            _destroyBlockEventRule = destroyBlockEventRule;
            _pathFindingUpdate = pathFindingUpdate;
        }

        public override IBlockRouter Create(ItemsList itemsList, IBlockRules blockRules, BlocksCommunication blocksCommunication, OutlineBlockView outlineBlockView, Transform blockPlacePoint, Transform cameraPivot, IBlockAnimation blockAnimation, LayerMask placeBlock, ICameraMovement cameraMovement, ISoundSource blockPlaceSoundSource, IBlockDamageSource blockDamageSource)
        {
            return new SandboxBlockRouter(input, blockRules, blocksCommunication, cameraMovement, inventory, itemsList, placeBlock, outlineBlockView, blockPlacePoint, cameraPivot, hand, blockAnimation, blockPlaceSoundSource, blockDamageSource, experienceEventsContainer, _placeBlockEventRule, _destroyBlockEventRule, _pathFindingUpdate);
        }
    }
}
