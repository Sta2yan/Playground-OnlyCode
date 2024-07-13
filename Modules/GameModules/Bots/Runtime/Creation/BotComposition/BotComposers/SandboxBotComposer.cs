using Agava.Blocks;
using Agava.Combat;
using Agava.Movement;
using Agava.Playground3D.Input;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class SandboxBotComposer : MonoBehaviour, IBotComposer
    {
        private const string BotEssensials = "_botEssensials";

        [Header("Character")]
        [SerializeField] private CombatCharacter _combatCharacter;
        [SerializeField] private CombatAnimator _combatAnimator;
        [SerializeField] private CollisionsDetector _collisionDetector;

        [Header("Movement")]
        [SerializeField] private BotMove _move;
        [SerializeField] private Jump _jump;
        [SerializeField] private Sprint _sprint;

        [Header("Behavior")]
        [SerializeField] private PathMovement _pathMovement;
        [SerializeField] private BehaviorTree _behaviorTree;
        [SerializeField] private SandboxTeamType _teamType;

        [Header("Inventory")]
        [SerializeField] private BotInventory _botInventory;
        [SerializeField] private Hand _hand;

        private BlocksCommunication _blocksCommunication;
        private ISandboxTeamList _teamList;

        public void Initialize(BlocksCommunication blocksCommunication, ISandboxTeamList teamList)
        {
            _blocksCommunication = blocksCommunication;
            _teamList = teamList;
        }

        public void ComposeBot()
        {
            BotBlocksGridInteraction blocksGridInteraction = new BotBlocksGridInteraction(_hand, _blocksCommunication);
            BotAttack botAttack = new BotAttack(_combatCharacter, _combatAnimator, _hand, _teamList);
            IBotInputMimic botInputMimic = new SandboxBotInputMimic(new CharacterMovementAdapter(_move, _jump, _sprint), blocksGridInteraction, _botInventory, botAttack);
            IBotMovementExecution movementExecution = new SandboxBotMovementExecution(botInputMimic, _collisionDetector);

            _pathMovement.Initialize(movementExecution);

            if (_teamList.TryGetTeam(_teamType, out ISandboxTeam team))
            {
                team.Add(_combatCharacter);
            }

            BotEssensials botEssensials = new BotEssensials(botInputMimic, _teamList);

            InitializeBT(new()
            {
                { BotEssensials, botEssensials },
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
