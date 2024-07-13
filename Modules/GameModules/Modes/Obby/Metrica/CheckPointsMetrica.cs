using System.Collections.Generic;
using System.Linq;
using Agava.Playground3D.YandexMetrica;
using UnityEngine;
using EventType = Agava.Playground3D.YandexMetrica.EventType;

namespace Agava.Playground3D.Obby.Metrica
{
    public class CheckPointsMetrica : MonoBehaviour
    {
        [SerializeField] private MetricaSource _metrica;

        private int _value;
        
        public void Send()
        {
            _value++;
            
            _metrica.Send(new MetricaEvent(EventType.LevelWin, "",
                        new Dictionary<string, object> {{"LevelName", _value}}));
        }
    }
}
