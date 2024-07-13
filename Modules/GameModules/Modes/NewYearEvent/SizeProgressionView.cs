using Agava.Utils;
using TMPro;
using UnityEngine;

namespace Agava.Playground3D.NewYearEvent
{
    public class SizeProgressionView : MonoBehaviour
    {
        [SerializeField] private SlicedFilledImage _progressImage;
        [SerializeField] private TMP_Text _count;

        public float ProgressImageFill => _progressImage.fillAmount;

        public void Render(float currentProgression, float maxProgression)
        {
            if (currentProgression < 0)
                currentProgression = 0;

            _progressImage.fillAmount = currentProgression / maxProgression;

            if (_count != null)
                _count.text = $"+{(int)(currentProgression * 100)}%";
        }
    }
}
