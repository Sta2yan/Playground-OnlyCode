namespace Agava.Playground3D.YandexMetrica
{
    public class YandexMetricaTarget : IMetricaTarget
    {
        public void Send(MetricaEvent eventId, bool withParameter = false)
        {
#if YANDEX_GAMES && !UNITY_EDITOR
            Agava.YandexMetrica.YandexMetrica.Send(eventId.ToString());
#endif
        }
    }
}
