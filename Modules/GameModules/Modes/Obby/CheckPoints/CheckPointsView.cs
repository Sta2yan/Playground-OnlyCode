using Agava.CheckPoints;
using Agava.Leaderboard;
using UnityEngine;

namespace Agava.Playground3D.Obby.CheckPoints
{
    public class CheckPointsView : MonoBehaviour
    {
        [SerializeField] private PointTrigger _point;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _particleSystem;

        private bool _activated;
        
        private void Update()
        {
            if (_activated) return;
            if (!_point.Complete) return;
            
            _activated = true;
            Execute();
        }

        private void Execute()
        {
            _animator.SetTrigger("Collect");
            _particleSystem.Play();
            LeaderboardSettings.AddScore(1);          
        }
    }
}
