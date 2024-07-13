using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class CollectingCharacter : MonoBehaviour
    {
        private const float MaxProgressFraction = 1.0f;
        private const float MilestoneProgressFraction = 0.01f;
        private const float Epsilon = 0.000001f;

        [SerializeField] private float _minScale = 0.6f;
        [SerializeField] private float _maxScale = 2.8f;
        [SerializeField] private float _currentScale = 0.6f;
        [SerializeField] private Transform _model;
        [SerializeField] private SizeProgressionView _sizeProgressionView;
        [SerializeField] private ParticleSystem _sizeUpEffect;

        private float _deltaScale;

        public float ProgressFraction { get; private set; }
        public bool MaxProgressReached => ProgressFraction == 1.0f;

        private void Awake()
        {
            _model.localScale = Vector3.one * _currentScale;
            _deltaScale = _maxScale - _minScale;

            ProgressFraction = CalculateProgressFraction();

            if (_sizeProgressionView != null)
                _sizeProgressionView.Render(ProgressFraction, MaxProgressFraction);
        }

        public void Collect(CollectableItemSpawnPoint item)
        {
            IncreaseSize(item.SizeBoostPercentage);
        }

        public void IncreaseSize(float sizeBoostPercentage)
        {
            if (MaxProgressReached)
                return;

            _currentScale += _deltaScale * sizeBoostPercentage;

            if (_currentScale > _maxScale)
                _currentScale = _maxScale;

            float newProgressFraction = CalculateProgressFraction();

            if (newProgressFraction - ProgressFraction > MilestoneProgressFraction)
            {
                ProgressFraction = newProgressFraction;

                if (_sizeProgressionView != null)
                    _sizeProgressionView.Render(ProgressFraction, MaxProgressFraction);

                if (_sizeUpEffect != null)
                    _sizeUpEffect.Play();

                _model.localScale = Vector3.one * _currentScale;
            }
        }

        private float CalculateProgressFraction()
        {
            float currentDeltaScale = _currentScale - _maxScale;

            if (Mathf.Abs(currentDeltaScale) < Epsilon)
                currentDeltaScale = 0;

            return 1.0f + currentDeltaScale / _deltaScale;
        }
    }
}
