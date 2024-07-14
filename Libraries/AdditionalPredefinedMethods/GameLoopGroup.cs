namespace Agava.AdditionalPredefinedMethods
{
    public class GameLoopGroup : IGameLoop
    {
        private readonly IGameLoop[] _gameLoops;

        public GameLoopGroup(params IGameLoop[] gameLoops)
        {
            _gameLoops = gameLoops;
        }

        public void Update(float deltaTime)
        {
            foreach (var gameLoop in _gameLoops)
                gameLoop.Update(deltaTime);
        }
    }
}
