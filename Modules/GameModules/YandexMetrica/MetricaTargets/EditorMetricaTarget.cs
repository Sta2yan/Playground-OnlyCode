using UnityEngine;

namespace Agava.Playground3D.YandexMetrica
{
    public class EditorMetricaTarget : IMetricaTarget
    {
        public void Send(MetricaEvent eventId, bool withParameter = false)
        {
            Debug.Log(eventId.ToString());
        }
    }
}
