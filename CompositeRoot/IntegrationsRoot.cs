using System.Collections;
using Agava.Advertisement;
using Agava.CheckPoints;
using Agava.Customization;
using Agava.Playground3D.YandexMetrica;
using System.Collections.Generic;
using Agava.Playground3D.RespawnLogic;
using UnityEngine;
using Agava.Playground3D.CoffeeBreak;
using Agava.Utils;
using UnityEngine.AddressableAssets;

namespace Agava.Playground3D.CompositeRoot
{
    public class IntegrationsRoot : CompositeRoot
    {
        [SerializeField] private MetricaSource _metricaSource;
        [SerializeField] private InterstitialLaunch _interstitialLaunch;
        [SerializeField] private bool _needInterstitialOnStart;
        [SerializeField] private SkinChooseButton _skinChooseButton;
        [SerializeField] private ObjectTransitionView _objectTransitionView;
        [SerializeField] private RespawnOnCheckPoints _respawn;

        private Advertisement.Advertisement _advertisement;
        private List<IMetricaTarget> _metricaTargets;
        private InterstitialOnStartYandexGames _interstitialOnStart;

        public override void Compose()
        {
            _metricaTargets = CreateMetricaTargets();
            _advertisement = CreateTargetAdvertisement();
            _interstitialLaunch.Initialize(_advertisement);

            #if YANDEX_GAMES
            StartCoroutine(LoadInterstitialOnStart());
            StartCoroutine(ExecuteInterstitialOnStart());
            #else
            if (_needInterstitialOnStart)
            {
                StartCoroutine(_interstitialLaunch.Execute());
            }
            #endif
            
            _metricaSource.Initialize(_metricaTargets);
            _objectTransitionView?.Initialize(_advertisement);
            _skinChooseButton?.Initialize(_advertisement);
            _respawn?.Initialize(_advertisement);
        }

        private IEnumerator LoadInterstitialOnStart()
        {
            var interstitialOnStart = Addressables.LoadAssetAsync<GameObject>("InterstitialOnStartYandexGame");

            yield return interstitialOnStart;

            _interstitialOnStart = Instantiate(interstitialOnStart.Result, transform).GetComponentInChildren<InterstitialOnStartYandexGames>();
        }

        private IEnumerator ExecuteInterstitialOnStart()
        {
            yield return new WaitUntil(() => _interstitialOnStart != null);

            if (_interstitialOnStart.Need) 
                StartCoroutine(_interstitialLaunch.Execute());
        }
        
        private Advertisement.Advertisement CreateTargetAdvertisement()
        {
            IAdvertisement reward;

#if YANDEX_GAMES && !UNITY_EDITOR
            reward = new YandexGamesReward();
#elif CAS_INTEGRATIONS
            reward = new CASReward();
#else
            reward = new MockReward();
#endif

            return new Advertisement.Advertisement(reward);
        }

        private List<IMetricaTarget> CreateMetricaTargets()
        {
            List<IMetricaTarget> metricaTargets = new List<IMetricaTarget>();

#if UNITY_EDITOR
            metricaTargets.Add(new EditorMetricaTarget());
#endif

#if YANDEX_GAMES && !UNITY_EDITOR
            metricaTargets.Add(new YandexMetricaTarget());
#endif

#if ANDROID_BUILD && FIREBASE
            metricaTargets.Add(new FirebaseMetricaTarget());
#endif

#if ANDROID_BUILD && APPMETRICA
            metricaTargets.Add(new AppMetricaTarget());
#endif

            return metricaTargets;
        }
    }
}
