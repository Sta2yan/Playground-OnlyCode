using Agava.Customization;
using Agava.Movement;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    public class MainMenuBotComposer : MonoBehaviour, IBotComposer
    {
        private const string BotEssensials = "_botEssensials";
        private const string Target = "_target";

        [Header("Appearance")]
        [SerializeField] private GameObject _model;
        [SerializeField] private NicknameView _nicknameView;
        [SerializeField] private SkinList[] _skinLists;

        [Header("Movement")]
        [SerializeField] private BotMove _move;
        [SerializeField] private Jump _jump;
        [SerializeField] private Sprint _sprint;

        [Header("Behavior")]
        [SerializeField] private PathMovement _pathMovement;
        [SerializeField] private BehaviorTree _behaviorTree;
        [SerializeField] private CharacterMovementAdapter _movement;
        [SerializeField] private CollisionsDetector _collisionsDetector;

        private bool _essentialsInitialized = false;

        private string _nickname;
        private Transform _targetSpot;

        public void Initialize(string nickname, Transform targetSpot)
        {
            _nickname = nickname;
            _targetSpot = targetSpot;
        }

        public void ComposeBot()
        {
            if (_essentialsInitialized == false)
                InitializeEssentials();

            _nicknameView.Render(_nickname);
            _behaviorTree.SetVariableValue(Target, _targetSpot);

            gameObject.SetActive(true);

            foreach (SkinList skinList in _skinLists)
                skinList.ChooseRandomSkin();

            _behaviorTree.EnableBehavior();
        }

        private void InitializeEssentials()
        {
            IBotInputMimic botInputMimic = new MainMenuBotInputMimic(new CharacterMovementAdapter(_move, _jump, _sprint));
            IBotMovementExecution movementExecution = new MainMenuBotMovementExecution(botInputMimic, _collisionsDetector);

            _pathMovement.Initialize(movementExecution);

            BotEssensials botEssensials = new BotEssensials(botInputMimic, null);
            _behaviorTree.SetVariableValue(BotEssensials, botEssensials);

            _essentialsInitialized = true;
        }
    }
}
