using DG.Tweening;
using UnityEngine;

namespace Agava.Utils
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _duration;

        private void Start()
        {
            transform.DOMove(_endPoint.position, _duration)
                     .SetEase(Ease.Linear)
                     .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
