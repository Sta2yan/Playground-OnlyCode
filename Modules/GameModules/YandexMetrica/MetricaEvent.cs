using System.Collections.Generic;

namespace Agava.Playground3D.YandexMetrica
{
    public struct MetricaEvent
    {
        public EventType EventType { get; private set; }
        public Dictionary<string, object> Parameters { get; private set; }
        public string EventName { get; private set; }

        public MetricaEvent(EventType eventType, string additionalName, Dictionary<string, object> parameters)
        {
            EventType = eventType;
            EventName = $"{EventType}{additionalName}";
            Parameters = parameters;
        }

        public override string ToString()
        {
            return $"{EventName}{string.Join("", Parameters.Values)}";
        }
    }
}
