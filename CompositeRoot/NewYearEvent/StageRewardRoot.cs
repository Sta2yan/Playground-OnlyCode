using Agava.AdditionalPredefinedMethods;
using Agava.Playground3D.NewYearEvent;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class StageRewardRoot : CompositeRoot
    {
        [SerializeField] private CollectingCharacter _targetCharacter;
        [SerializeField] private MonoBehaviour _stageRewardObject;
        [SerializeField] private FinishPanel _finishPanel;

        private IGameLoop _gameLoop;
        private IStageReward _stageReward => (IStageReward)(_stageRewardObject);

        private void OnValidate()
        {
            if (_stageRewardObject && _stageRewardObject is not IStageReward)
            {
                Debug.LogError(nameof(_stageRewardObject) + " needs to implement " + nameof(IStageReward));
                _stageRewardObject = null;
            }
        }

        public override void Compose()
        {
            StageRewardRouter stageRewardRouter = new StageRewardRouter(_targetCharacter, _finishPanel, _stageReward);
            _gameLoop = new GameLoopGroup(stageRewardRouter);
        }

        private void Update()
        {
            _gameLoop?.Update(Time.deltaTime);
        }
    }
}
