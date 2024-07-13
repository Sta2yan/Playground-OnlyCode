using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Agava.Playground3D.Bots
{
    internal class WaitOneTime : Action
    {
        public SharedFloat Min = 5;
        public SharedFloat Max = 10;

        private bool _waited;
        private float _waitDuration;
        private float _startTime;
        private float _pauseTime;

        public override void OnStart()
        {
            _startTime = Time.time;
            _waitDuration = _waited ? 0 : Random.Range(Min.Value * 10f, Max.Value * 10f) / 10f;
            
            _waited = true;
        }

        public override TaskStatus OnUpdate()
        {
            if (_startTime + _waitDuration < Time.time)
                return TaskStatus.Success;
            
            return TaskStatus.Running;
        }

        public override void OnPause(bool paused)
        {
            if (paused)
                _pauseTime = Time.time;
            else
                _startTime += Time.time - _pauseTime;
        }
    }
}