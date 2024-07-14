using Agava.Input;
using Agava.Playground3D.Blocks;
using UnityEngine;
using Agava.Playground3D.Input;
using Agava.Inventory;
using Agava.Blocks;
using Agava.Movement;
using Agava.Playground3D.Items;
using Agava.Audio;
using Agava.ExperienceSystem;

namespace Agava.Playground3D.BlockRouterFactories
{
    public abstract class BlockRouterFactory
    {
        protected readonly IInput input;
        protected readonly IInventory inventory;
        protected readonly IInventoryView inventoryView;
        protected readonly Hand hand;
        protected readonly IBlockDamageSource blockDamageSource;
        protected readonly ExperienceEventsContainer experienceEventsContainer;

        protected BlockRouterFactory(IInput input, IInventory inventory,
            IInventoryView inventoryView, Hand hand, ExperienceEventsContainer experienceEventsContainer)
        {
            this.input = input;
            this.inventory = inventory;
            this.inventoryView = inventoryView;
            this.hand = hand;
            this.experienceEventsContainer = experienceEventsContainer;
        }

        public abstract IBlockRouter Create(ItemsList itemsList, IBlockRules blockRules, BlocksCommunication blocksCommunication, OutlineBlockView outlineBlockView, Transform blockPlacePoint, Transform cameraPivot, IBlockAnimation blockAnimation, LayerMask placeBlock, ICameraMovement cameraMovement, ISoundSource blockPlaceSoundSource, IBlockDamageSource blockDamageSource);
    }
}
