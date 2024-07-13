using TMPro;
using UnityEngine;

namespace Agava.Time
{
    public class TimeCountView : MonoBehaviour
    {
        private const int SecondsInMinute = 60;
        
        [SerializeField] private TMP_Text _count;
        
        private float _currentTime;
        private bool _active = true;

        public float Value => _currentTime;
        
        private void Update()
        {
            if (_active == false)
                return;
            
            var deltaTime = UnityEngine.Time.deltaTime;
            _currentTime += deltaTime;

            _count.text = $"{(int) _currentTime / SecondsInMinute:00}:{(int) _currentTime % SecondsInMinute:00}";
        }

        public void Stop()
        {
            _active = false;
        }
    }
}
