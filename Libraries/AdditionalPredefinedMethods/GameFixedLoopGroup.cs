namespace Agava.AdditionalPredefinedMethods
{
    public class GameFixedLoopGroup : IGameFixedLoop
    {
        private readonly IGameFixedLoop[] _gameLoops;

        public GameFixedLoopGroup(params IGameFixedLoop[] gameLoops)
        {
            _gameLoops = gameLoops;
        }
        public void FixedUpdate()
        {
            foreach (var gameLoop in _gameLoops)
                gameLoop.FixedUpdate();
        }
    }
}