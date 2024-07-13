using TMPro;
using UnityEngine;

namespace Agava.CheckPoints
{
    public class PointsView : MonoBehaviour
    {
        [SerializeField] private PointsContainer _container;
        [SerializeField] private TMP_Text _text;

        private void Update()
        {
            int count = 0;

            for (int i = 0; i < _container.Points.Count; i++)
            {
                if (_container.Points[i].Complete)
                {
                    count++;
                }
            }
            
            _text.text = count.ToString();
        }
    }
}