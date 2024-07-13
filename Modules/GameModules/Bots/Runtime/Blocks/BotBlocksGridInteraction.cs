using Agava.Playground3D.Input;
using Agava.Playground3D.Items;
using UnityEngine;
using Agava.Blocks;

namespace Agava.Playground3D.Bots
{
    internal class BotBlocksGridInteraction : IBotBlocksGridInteraction
    {
        private readonly BlocksCommunication _blocksCommunication;
        private readonly Hand _hand;

        private float _nextRemoveAttemptTime;

        public BotBlocksGridInteraction(
            Hand hand,
            BlocksCommunication blocksCommunication)
        {
            _blocksCommunication = blocksCommunication;
            _hand = hand;
            _nextRemoveAttemptTime = 0;
        }

        public bool TryPlaceBlock(Vector3 position)
        {
            return TryGetBlock(out var blockItem, out var blockTemplate) && _blocksCommunication.TryPlaceNear(blockTemplate, position, blockItem.Id, blockItem.Health);
        }

        public bool TryRemoveBlock(Vector3 position)
        {
            if (_hand.CurrentItem.TryConvertTo(out IPick pick) == false)
                return false;

            if (Time.time < _nextRemoveAttemptTime)
                return false;

            _nextRemoveAttemptTime = Time.time + pick.DigDelay;

            if (_blocksCommunication.HasBlockIn(position) == false)
                return false;

            _blocksCommunication.ApplyDamage(position, pick.Damage);

            return _blocksCommunication.HasBlockIn(position) == false;
        }

        private bool TryGetBlock(out IBlock item, out Block block)
        {
            item = default;
            block = default;

            if (_hand.CurrentItem.TryConvertTo(out item) == false)
                return false;

            if (item.BlockTemplate.TryGetComponent(out block) == false)
                return false;

            return true;
        }
    }
}
