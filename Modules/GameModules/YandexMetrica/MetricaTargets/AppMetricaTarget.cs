namespace Agava.Playground3D.YandexMetrica
{
    public class AppMetricaTarget : IMetricaTarget
    {
        public void Send(MetricaEvent eventId, bool withParameter = false)
        {
#if ANDROID_BUILD && APPMETRICA
            if (withParameter)
            {
                AppMetrica.AppMetrica.Instance.ReportEvent(eventId.EventName, eventId.Parameters);
            }
            else
            {
                AppMetrica.AppMetrica.Instance.ReportEvent(eventId.ToString());
            }
#endif
        }
    }
}
