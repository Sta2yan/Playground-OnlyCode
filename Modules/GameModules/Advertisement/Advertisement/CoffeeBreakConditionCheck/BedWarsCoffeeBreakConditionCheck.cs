using UnityEngine;
using Agava.Utils;
using Agava.Movement;

namespace Agava.Playground3D.CoffeeBreak
{
    public class BedWarsCoffeeBreakConditionCheck : MonoBehaviour, IConditionCheck
    {
        [SerializeField] private Jump _jump;
        [SerializeField] private string _islandCollidersTag;

        public bool ConditionMet => _jump.Grounded &&
            (_jump.LastHitCollider == null ? false : _jump.LastHitCollider.gameObject.tag == _islandCollidersTag);

        public void Check() { }

        public void Uncheck() { }
    }
}
