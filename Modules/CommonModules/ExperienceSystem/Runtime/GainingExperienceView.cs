using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agava.ExperienceSystem
{
    public class GainingExperienceView : MonoBehaviour
    {
        private const float DistanceToDownScaleText = 200f;

        [SerializeField] private TMP_Text _template;
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private float _speedToTargetPoint;
        [SerializeField] private float _speedToScaleUp;
        [SerializeField, Min(0)] private Vector2 _borderRelativeResolution;

        public void Execute(int experienceCount, Action onFinish)
        {
            Vector2 spawnPoint = RandomSpawnPointOnInterface();

            var instance = Instantiate(_template, spawnPoint, Quaternion.identity, _root.transform);
            instance.text = "+" + experienceCount;
            instance.color = RandomColor();

            StartCoroutine(ExecuteAnimation(instance.gameObject, experienceCount, onFinish));
        }

        private IEnumerator ExecuteAnimation(GameObject instance, int experience, Action onFinish)
        {
            instance.transform.localScale = Vector3.zero;
            float resolutionCorrect = Screen.width - Screen.height > 0
                ? Screen.width / Screen.height
                : Screen.height / Screen.width;
            Vector3 targetScale = ScaleBy(experience);

            while (instance.transform.localScale != targetScale)
            {
                float stepScale = _speedToScaleUp * resolutionCorrect * Time.unscaledDeltaTime;

                instance.transform.localScale =
                    Vector3.MoveTowards(instance.transform.localScale, targetScale, stepScale);

                yield return null;
            }

            while (Mathf.Abs(Vector3.Distance(instance.transform.position, _targetPoint.position)) > 10f)
            {
                float stepTransform = _speedToTargetPoint * resolutionCorrect * Time.unscaledDeltaTime;

                instance.transform.position =
                    Vector3.MoveTowards(instance.transform.position, _targetPoint.position, stepTransform);

                if ((_targetPoint.position - instance.transform.position).magnitude < DistanceToDownScaleText)
                {
                    float stepScale = _speedToScaleUp * resolutionCorrect * Time.unscaledDeltaTime;

                    instance.transform.localScale =
                        Vector3.MoveTowards(instance.transform.localScale, Vector3.zero, stepScale);
                }

                yield return null;
            }

            onFinish.Invoke();

            Destroy(instance);
        }

        private Vector3 RandomSpawnPointOnInterface()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float spawnAreaWidth = screenWidth * _borderRelativeResolution.x;
            float spawnAreaHeight = screenHeight * _borderRelativeResolution.y;
            
            return new Vector2(Random.Range(screenWidth / 6, screenWidth / 2 - screenWidth / 4),
                Random.Range(screenHeight / 2 - spawnAreaHeight / 2, screenHeight / 2 + spawnAreaHeight / 2));
        }

        private Vector3 ScaleBy(int experience)
            => (Vector3.one * (experience / 10f * 1.5f + 1f)).x <= 3 ? Vector3.one * (experience / 10f * 1.5f + 1f) : new Vector3(3,3,3);

        private Color RandomColor()
           => new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
