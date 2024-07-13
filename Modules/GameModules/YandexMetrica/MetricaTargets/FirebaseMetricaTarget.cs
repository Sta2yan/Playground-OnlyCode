#if FIREBASE
using Firebase.Analytics;
#endif

namespace Agava.Playground3D.YandexMetrica
{
    public class FirebaseMetricaTarget : IMetricaTarget
    {
        public void Send(MetricaEvent eventId, bool withParameter = false)
        {
#if ANDROID_BUILD && FIREBASE

            switch (eventId.EventType)
            {
                case EventType.LevelStart:
                    {
                        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart, new Parameter("level_name", eventId.Parameters["LevelName"].ToString()));
                        break;
                    }

                case EventType.LevelWin:
                    {
                        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, new Parameter[]
                        {
                            new Parameter("level_name", eventId.Parameters["LevelName"].ToString()),
                            new Parameter("success", "true"),
                        });
                        break;
                    }

                default:
                    {
                        FirebaseAnalytics.LogEvent(eventId.ToString());
                        break;
                    }
            }
#endif
        }
    }
}
