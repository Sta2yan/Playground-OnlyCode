namespace Agava.Playground3D.YandexMetrica
{
    public interface IMetricaTarget
    {
        void Send(MetricaEvent eventId, bool withParameter = false);
    }
}
