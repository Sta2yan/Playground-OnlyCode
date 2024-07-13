using System;
using UnityEngine.Scripting;

namespace Assets.Source.Plauground.IAp
{
    [Serializable]
    public class IAPEntity : IIApEntity
    {
        [Preserve] public string Id;
        [Preserve] public string Name;
        [Preserve] public string Description;
        [Preserve] public float Price;
        [Preserve] public bool IsBought;

        public bool Bought => IsBought;
        public string PriceValue { get; private set; }

        public IAPEntity(string id)
        {
            Id = string.IsNullOrEmpty(id) == false ? id : throw new ArgumentNullException(id);
            IsBought = false;
        }

        public void SetPrice(string price)
        {
            PriceValue = string.IsNullOrEmpty(price) ? throw new ArgumentOutOfRangeException(nameof(price)) : price;
        }

        public void SetBought()
        {
            IsBought = true;
        }
    }

    public interface IIApEntity
    {
        public string PriceValue { get; }
        public bool Bought { get; }

        public void SetBought();
    }
}
