using UnityEngine;

namespace Agava.Movement
{
    public class Sprint : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _value = 1.5f;
        [SerializeField] private Move _move;
        
        internal bool Active { get; private set; }
        
        private void Update()
        {
            TrySprinting();
        }

        public bool TryEnableSprint(float horizontal, float vertical)
        {
            if (Mathf.Abs(horizontal) > .5f || Mathf.Abs(vertical) > .5f)
                Active = true;

            return Active;
        }

        public void DisableSprint()
        {
            Active = false;
        }
        
        private void TrySprinting()
        {
            if (_move.Moving == false)
            {
                Active = false;
                return;
            }

            if (_move.CanMove == false)
                return;

            _move.ChangeSpeedMultiplier(Active ? _value : 1f);
        }
    }
}
