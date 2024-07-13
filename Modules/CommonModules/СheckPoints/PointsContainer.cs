using Agava.ExperienceSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.CheckPoints
{
    public class PointsContainer : MonoBehaviour
    {
        [SerializeField] private List<PointTrigger> _points;

        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _pointTriggeredEventRule;

        public bool CanDisplaceObjectToPoint => _points.FindLast(point => point.Complete) != null;
        public IReadOnlyList<PointTrigger> Points => _points;

        public void Initialize(ExperienceEventsContainer experienceEventsContainer, ExperienceEventRule pointTriggeredEventRule)
        {
            _experienceEventsContainer = experienceEventsContainer;
            _pointTriggeredEventRule = pointTriggeredEventRule;

            foreach (PointTrigger pointTrigger in _points)
                pointTrigger.Initialize(_experienceEventsContainer, _pointTriggeredEventRule);
        }

        public bool TryDisplaceObjectToPoint(out Vector3 position)
        {
            PointTrigger pointTrigger = _points.FindLast(point => point.Complete);
            position = Vector3.zero;

            if (pointTrigger == null)
                return false;

            Transform lastCompletePoint = pointTrigger.transform;
            position = lastCompletePoint.position;
            return true;

        }

        public void DisplaceToLastCompletePoint(Transform transform, Vector3 offset)
        {
            Transform lastCompletePoint = _points.FindLast(point => point.Complete).transform;

            transform.position = lastCompletePoint.position + offset;
        }
    }
}
