using System;
using Agava.Save;

namespace Agava.ExperienceSystem
{
    public class ExperiencePlayer
    {
        private const string SaveKey = "PlayerExperienceValue";

        public int Value { get => SaveFacade.GetInt(SaveKey); private set => SaveFacade.SetInt(SaveKey, value); } 

        public void AddScore(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value) + "can't be less 0!");

            Value += value;
        }
    }
}
