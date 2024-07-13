using Agava.Blocks;
using Agava.Playground3D.Input;
using Agava.Playground3D.Items;
using UnityEngine;
using UnityEngine.UI;
using Agava.Playground3D.BlockRouterFactories;
using Agava.AdditionalPredefinedMethods;
using Agava.Playground3D.Blocks;
using Agava.Movement;
using Agava.Audio;

namespace Agava.Playground3D.CompositeRoot
{
    public class BlocksRoot : CompositeRoot
    {
        private const float BlockPlaceDistance = 5f;

        [SerializeField] private Transform _blockPlacePoint;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Transform _character;
        [SerializeField] private LockForPlaceBlocks _lockForPlaceBlocks;
        [SerializeField] private OutlineBlockView _outlineBlockView;
        [SerializeField] private BoxGridAvailableArea _gridAvailableArea;
        [SerializeField] private GridSetup _gridSetup;
        [SerializeField] private GridBlockDrawRule _blockDrawRule;
        [SerializeField] private Transform _healthViewRoot;
        [SerializeField] private Image _blockHealth;
        [SerializeField] private BlockDamageSource _blockDamageSource;
        [SerializeField] private LayerMask _placeBlock;
        [SerializeField] private BlockAnimator _blockAnimator;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private bool _canRemoveStartUpBlocks = true;
        [SerializeField] private SoundSource _blockPlaceSoundSource;

        private ItemsList _itemsList;
        private BlockRouterFactory _factory;

        private IGameLoop _gameLoop;

        public BlocksCommunication BlocksCommunication { get; private set; }

        public void Initialize(ItemsList itemlist, BlockRouterFactory factory)
        {
            _itemsList = itemlist;
            _factory = factory;
        }

        public override void Compose()
        {
            var grid = new BlockGrid(_gridAvailableArea);
            var gridView = new GridView(_itemsList, _blockDrawRule);
            var blockHealthView = new BlockHealthView(_healthViewRoot, _blockHealth);
            BlocksCommunication = new BlocksCommunication(grid, gridView, blockHealthView, _canRemoveStartUpBlocks);
            var blockRules = new BlockRules(_character, grid, _lockForPlaceBlocks, BlockPlaceDistance);

            _outlineBlockView.Init(grid);
            _gridSetup.Initialize(BlocksCommunication, _itemsList);
            _gridSetup.UploadBlocks();

            IBlockRouter blockRouter = _factory.Create(_itemsList, blockRules, BlocksCommunication, _outlineBlockView, _blockPlacePoint, _cameraPivot, _blockAnimator, _placeBlock, _cameraMovement, _blockPlaceSoundSource, _blockDamageSource);

            _gameLoop = new GameLoopGroup(blockRouter as IGameLoop, gridView, blockHealthView);
        }

        private void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }
    }
}
