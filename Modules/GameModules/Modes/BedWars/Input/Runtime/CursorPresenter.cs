using Agava.Combat;
using Agava.Input;
using Agava.Inventory;
using Agava.Movement;
using Agava.Playground3D.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.Input
{
    public class CursorPresenter : MonoBehaviour
    {
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private Agava.Shop.Shop _shop;
        [SerializeField] private InventoryView _inventory;
        [SerializeField] private Image _crosshair;
        [SerializeField] private CoffeeBreak.CoffeeBreak _coffeeBreak;

        private ITeamList _teamList;
        private BedWarsMovementRouter _movementInputRouter;
        private IMovementRouter _movement;
        private BlockInputRouter _blockInputRouter;
        private CombatInputRouter _combatInputRouter;

        private bool _lastNeedCrosshair;
        private bool _initialized;

        private void Awake()
        {
            SetActiveCrosshair(false);
        }

        private void Update()
        {
            if (_initialized == false)
                return;

            if (_teamList != null)
            {
                if (_teamList.PlayerWin)
                {
                    SetActiveCrosshair(false);
                    _cameraMovement.SetActiveRotation(false);
                    _movementInputRouter.SetActive(false);
                    _combatInputRouter.SetActive(false);
                    _blockInputRouter.SetActive(false);

                    return;
                }
            }

            var needInput = true;

            if (_shop != null && _inventory != null)
                needInput = _shop.Opened == false && _inventory.Opened == false;

#if !CAS_INTEGRATIONS
            if (_coffeeBreak != null)
                needInput &= !_coffeeBreak.Active;
#endif

            bool needCrosshair;

            if (_combatInputRouter != null)
                needCrosshair = _cameraMovement.FirstPersonPerspective && needInput || _combatInputRouter.Zooming;
            else
                needCrosshair = _cameraMovement.FirstPersonPerspective && needInput;

            if (needCrosshair == _lastNeedCrosshair)
                return;

            SetActiveCrosshair(needCrosshair);

            _cameraMovement?.SetActiveRotation(needInput);
            _combatInputRouter?.SetActive(needInput);
            _blockInputRouter?.SetActive(needInput);
        }

        internal void Initialize(ITeamList teamList, IMovementRouter movementInputRouter, BlockInputRouter blockInputRouter, CombatInputRouter combatInputRouter)
        {
            _teamList = teamList;
            _movement = movementInputRouter;
            _blockInputRouter = blockInputRouter;
            _combatInputRouter = combatInputRouter;

            _initialized = true;
        }

        internal void SetActiveCrosshair(bool value)
        {
            _lastNeedCrosshair = value;
            _crosshair.enabled = value;
            
            if (_movement?.Input is StandaloneInput)
            {
                Cursor.visible = value == false;
                Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.Confined;
            }
            else
            {
                _lastNeedCrosshair = false;
                _crosshair.enabled = false;
            }
        }
    }
}
