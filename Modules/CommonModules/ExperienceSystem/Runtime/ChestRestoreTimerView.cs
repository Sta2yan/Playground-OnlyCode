using System;
using TMPro;
using UnityEngine;

namespace Agava.ExperienceSystem
{
    public class ChestRestoreTimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ChestRestoreTimer _chestRestoreTimer;
        [SerializeField] private ExperienceChest _experienceChest;

        private void Update()
        {
            _text.gameObject.SetActive(_experienceChest.CanActivate);

            if (_chestRestoreTimer.Activate)
            {
                var time = _chestRestoreTimer.TimeToReward - DateTime.Now;

                _text.text = _chestRestoreTimer.Activate ? $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}" : "";
            }
            else
            {
                _text.text = "";
            }
        }
    }
}
