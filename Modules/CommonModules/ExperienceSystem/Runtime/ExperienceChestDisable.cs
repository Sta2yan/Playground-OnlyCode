using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class ExperienceChestDisable : MonoBehaviour
    {
        [SerializeField] private ExperienceChest _experienceChest;

        private void Awake()
        {
            if (_experienceChest.Collect == false) return;
            
            _experienceChest.gameObject.SetActive(false);
        }
    }
}
