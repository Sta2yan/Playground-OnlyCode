using Agava.Movement;
using Agava.Playground3D.Input;
using Agava.Blocks;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Agava.Combat;
using System.Collections.Generic;
using System.Linq;
using Agava.Customization;
using Agava.Playground3D.BedWars.Combat;

namespace Agava.Playground3D.Bots
{
    internal class BedWarsBotComposer : MonoBehaviour, IBotComposer
    {
        private const string PredefinedPathsVariable = "_predefinedPathList";
        private const string TeamIslandVariable = "_teamIsland";
        private const string CurrentIslandVariable = "_currentIsland";
        private const string MiddleIslandVariable = "_middleIsland";
        private const string EnemyTeamIslandsVariable = "_enemyTeamIslandList";
        private const string SideIslandsVariable = "_sideIslandList";
        private const string MiddleIslandPoints = "_middleIslandPointList";
        private const string BotEssensials = "_botEssensials";

        [Header("Appearance")]
        [SerializeField] private NicknameView _nicknameView;
        [SerializeField] private SkinList[] _skinLists;
        [Header("Character")]
        [SerializeField] private CombatCharacter _combatCharacter;
        [SerializeField] private MovementAnimator _movementAnimator;
        [SerializeField] private CombatAnimator _combatAnimator;
        [SerializeField] private BotMovement _movement;
        [SerializeField] private CollisionsDetector _collisionsDetector;
        [Header("Behavior")]
        [SerializeField] private PathMovement _pathMovement;
        [SerializeField] private BehaviorTree _behaviorTree;
        [Header("Inventory")]
        [SerializeField] private BotInventory _botInventory;
        [SerializeField] private Hand _hand;

        private string _nickname;
        private BlocksCommunication _blocksCommunication;
        private PredefinedPathObject[] _predefinedPathObjects;
        private GameObject _teamIsland;
        private GameObject _middleIsland;
        private GameObject[] _teamIslands;
        private GameObject[] _sideIslands;
        private Transform[] _middleIslandPoints;
        private IBedWarsTeamList _teamList;

        public void Initialize(string nickname,
            BlocksCommunication blocksCommunication,
            PredefinedPathObject[] predefinedPathObjects,
            GameObject teamIsland,
            GameObject middleIsland,
            GameObject[] teamIslands,
            GameObject[] sideIslands,
            Transform[] middleIslandPoints,
            IBedWarsTeamList teamList)
        {
            _nickname = nickname;
            _blocksCommunication = blocksCommunication;
            _predefinedPathObjects = predefinedPathObjects;
            _teamIsland = teamIsland;
            _middleIsland = middleIsland;
            _teamIslands = teamIslands;
            _sideIslands = sideIslands;
            _middleIslandPoints = middleIslandPoints;
            _teamList = teamList;
        }

        public void ComposeBot()
        {
            _movement.Initialize(_movementAnimator);
            BotBlocksGridInteraction blocksGridInteraction = new BotBlocksGridInteraction(_hand, _blocksCommunication);
            BotAttack botAttack = new BotAttack(_combatCharacter, _combatAnimator, _hand, _teamList);
            IBotInputMimic botInputMimic = new BedWarsBotInputMimic(_movement, blocksGridInteraction, _botInventory, botAttack);
            BedWarsBotMovementExecution movementExecution = new BedWarsBotMovementExecution(botInputMimic, _collisionsDetector);

            _pathMovement.Initialize(movementExecution);

            List<GameObject> enemyTeamIslandList = _teamIslands.Where(island => island != _teamIsland).ToList();

            _nicknameView.Render(_nickname);

            foreach (SkinList skinList in _skinLists)
                skinList.ChooseRandomSkin();

            BotEssensials botEssensials = new BotEssensials(botInputMimic, _teamList);

            void InitializeBT(Dictionary<string, object> variableValues)
            {
                foreach (var variableValue in variableValues)
                    _behaviorTree.SetVariableValue(variableValue.Key, variableValue.Value);
            }

            InitializeBT(new()
            {
                { PredefinedPathsVariable,  new List<PredefinedPathObject>(_predefinedPathObjects) },
                { TeamIslandVariable, _teamIsland },
                { CurrentIslandVariable, _teamIsland },
                { MiddleIslandVariable, _middleIsland },
                { EnemyTeamIslandsVariable, enemyTeamIslandList },
                { SideIslandsVariable, new List<GameObject>(_sideIslands) },
                { MiddleIslandPoints, new List<Transform>(_middleIslandPoints) },
                { BotEssensials, botEssensials }
            });

            _behaviorTree.EnableBehavior();
        }
    }
}
