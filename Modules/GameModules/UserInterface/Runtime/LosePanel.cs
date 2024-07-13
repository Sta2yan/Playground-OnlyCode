using TMPro;
using UnityEngine;

namespace Agava.Playground3D.UserInterface
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Visualize(float timeToRespawn)
        {
            _count.text = timeToRespawn.ToString("F1");
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
