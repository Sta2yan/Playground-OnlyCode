using Agava.Utils;
using TMPro;
using UnityEngine;

namespace Agava.Combat
{
    internal class HealthView : MonoBehaviour, IHealthView
    {
        [SerializeField] private SlicedFilledImage _progressImage;
        [SerializeField] private TMP_Text _count;

        public float ProgressImageFill => _progressImage.fillAmount;
        
        public void Render(int maxHealth, int currentHealth)
        {
            _progressImage.fillAmount = (float)currentHealth / maxHealth;

            if (currentHealth < 0)
                currentHealth = 0;
            
            if (_count != null)
                _count.text = currentHealth.ToString();
        }
    }
}