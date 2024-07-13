using System.Collections.Generic;
using UnityEngine;
using Agava.Playground3D.CombatRouterFactories;
using Agava.Combat;
using Agava.Playground3D.Items;
using Agava.AdditionalPredefinedMethods;
using Agava.Playground3D.Combat;
using Agava.Playground3D.Input;
using Agava.Inventory;
using Agava.Movement;
using Agava.Audio;
using Agava.Utils;
using UnityEngine.UI;

namespace Agava.Playground3D.CompositeRoot
{
    public class CombatRoot : CompositeRoot
    {
        private const float DelayToRespawn = 1f;

        [SerializeField] private CombatCharacter _playerCombatCharacter;
        [SerializeField] private BulletItem _crossbowBullet;
        [SerializeField] private CombatAnimator _combatAnimator;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SoundSource _swordSwingSoundSource;
        [SerializeField] private ObjectMoveToMouse _crosshair;
        [Header("Utils")]
        [SerializeField] private RedBlood _redBlood;

        private CombatRouterFactory _factory;
        private IInventory _inventory;
        private DelayedRespawn _delayedRespawn;
        private IInventoryView _inventoryView;
        private ICombatRouter _combatRouter;
        private IGameLoop _gameLoop;
        private bool _isMobile;

        public void Initialize(IInventory inventory, IInventoryView inventoryView, bool isMobile, CombatRouterFactory factory)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
            _factory = factory;
            _isMobile = isMobile;
        }

        public override void Compose()
        {
            var crossbowMagazine = new Magazine(_crossbowBullet, _inventory, _inventoryView);
            var pistolMagazine = new InfinityMagazine();
            var shotgunMagazine = new InfinityMagazine();
            var machineMagazine = new InfinityMagazine();

            Dictionary<string, IMagazine> magazines = new Dictionary<string, IMagazine>
            {
                { nameof(CrossbowGun), crossbowMagazine },
                { nameof(PistolGun), pistolMagazine },
                { nameof(Shotgun), shotgunMagazine },
                { nameof(MachineGun), machineMagazine },
            };

            _combatRouter = _factory.Create(magazines, _playerCombatCharacter, _cameraMovement, _combatAnimator, _swordSwingSoundSource, _crosshair, _isMobile, _redBlood);
            _gameLoop = new GameLoopGroup(_combatRouter as IGameLoop);
            _delayedRespawn = new DelayedRespawn(this, DelayToRespawn);
        }

        public void Update()
        {
            _gameLoop?.Update(Time.deltaTime);

            if (_playerCombatCharacter.Alive == false)
                _delayedRespawn.Respawn(_playerCombatCharacter, _spawnPoint.position);
        }
    }
}
