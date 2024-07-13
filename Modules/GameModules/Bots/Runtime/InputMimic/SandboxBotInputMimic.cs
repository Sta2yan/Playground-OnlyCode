using Agava.Movement;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class SandboxBotInputMimic : IBotInputMimic
    {
        private readonly CharacterMovementAdapter _movement;
        private readonly IBotBlocksGridInteraction _gridInteraction;
        private readonly IBotInventory _inventory;
        private readonly IBotAttack _attack;

        public SandboxBotInputMimic(CharacterMovementAdapter movementAdapter, IBotBlocksGridInteraction gridInteraction, IBotInventory botInventory, IBotAttack attack)
        {
            _movement = movementAdapter;
            _gridInteraction = gridInteraction;
            _inventory = botInventory;
            _attack = attack;
        }

        public bool TryAttack()
        {
            _inventory.EquipWeapon();
            return _attack.TryAttack();
        }

        public bool TryJump()
        {
            return _movement.TryJump();
        }

        public void Stop()
        {
            _movement.TryMove(0, 0);
            _movement.DisableSprint();
        }

        public bool TryPlaceBlock(Vector3 position)
        {
            _inventory.EquipBlock();
            return _gridInteraction.TryPlaceBlock(position);
        }

        public void RemoveBlock(Vector3 position)
        {
            _inventory.EquipPickaxe();
            //_gridInteraction.TryRemoveBlock(position);
        }

        public bool TryMove(float horizontal, float vertical)
        {
            return _movement.TryMove(horizontal, vertical);
        }

        public bool TryEnableSprint(float horizontal, float vertical)
        {
            return _movement.TryEnableSprint(horizontal, vertical);
        }

        public void DisableSprint()
        {
            _movement.DisableSprint();
        }

        public void LookAt(Transform target)
        {
            _movement.LookAt(target);
        }
    }
}
