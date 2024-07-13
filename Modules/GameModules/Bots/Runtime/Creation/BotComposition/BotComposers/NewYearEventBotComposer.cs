using Agava.Combat;
using Agava.Customization;
using Agava.Movement;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;
using Agava.Playground3D.NewYearEvent;

namespace Agava.Playground3D.Bots
{
    public class NewYearEventBotComposer : MonoBehaviour, IBotComposer
    {
        private const string BotEssensials = "_botEssensials";
        private const string Container = "_spawnPointsContainer";

        [Header("Character")]
        [SerializeField] private CombatCharacter _combatCharacter;
        [SerializeField] private CollisionsDetector _collisionDetector;
        [SerializeField] private SkinList[] _skinLists;

        [Header("Movement")]
        [SerializeField] private BotMove _move;
        [SerializeField] private Jump _jump;
        [SerializeField] private Sprint _sprint;

        [Header("Behavior")]
        [SerializeField] private PathMovement _pathMovement;
        [SerializeField] private BehaviorTree _behaviorTree;

        private CollectableItemsBotContainer _container;

        public void Initialize(CollectableItemsBotContainer container)
        {
            _container = container;
        }

        public void ComposeBot()
        {
            foreach (SkinList skinList in _skinLists)
                skinList.ChooseRandomSkin();

            IBotInputMimic botInputMimic = new NewYearEventBotInputMimic(new CharacterMovementAdapter(_move, _jump, _sprint));
            IBotMovementExecution movementExecution = new NewYearEventBotMovementExecution(botInputMimic, _collisionDetector);

            _pathMovement.Initialize(movementExecution);

            BotEssensials botEssensials = new BotEssensials(botInputMimic, null);

            InitializeBT(new()
            {
                { BotEssensials, botEssensials },
                { Container, _container },
            });

            void InitializeBT(Dictionary<string, object> variableValues)
            {
                foreach (var variableValue in variableValues)
                    _behaviorTree.SetVariableValue(variableValue.Key, variableValue.Value);
            }

            _behaviorTree.EnableBehavior();
        }
    }
}
