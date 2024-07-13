using Agava.AdditionalPredefinedMethods;
using UnityEngine;
using Agava.Playground3D.PathFinding;

namespace Agava.Playground3D.CompositeRoot
{
    public class PathfindingRoot : CompositeRoot
    {
        [SerializeField] private float _updateDelay;

        private PathFindingUpdate _pathFindingUpdate;
        private IGameLoop _gameLoop;

        public void Initialize(PathFindingUpdate pathFindingUpdate)
        {
            _pathFindingUpdate = pathFindingUpdate;
        }

        public override void Compose()
        {
            PathFindingRouter pathFindingRouter = new PathFindingRouter(_pathFindingUpdate, _updateDelay);
            _gameLoop = new GameLoopGroup(pathFindingRouter as IGameLoop);
        }

        private void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }
    }
}
