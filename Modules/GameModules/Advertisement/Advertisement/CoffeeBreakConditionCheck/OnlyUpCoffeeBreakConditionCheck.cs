using UnityEngine;
using Agava.Utils;
using Agava.Movement;

namespace Agava.Playground3D.CoffeeBreak
{
    public class OnlyUpCoffeeBreakConditionCheck : MonoBehaviour, IConditionCheck
    {
        [SerializeField] private Jump _jump;

        public bool ConditionMet => _jump.Grounded;

        public void Check() { }

        public void Uncheck() { }
    }
}
