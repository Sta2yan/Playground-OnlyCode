using System;

namespace Assets.Source.Plauground.IAp
{
    public class SubscriptionData : ISubscriptionData
    {
        private readonly Action<Action> _purchaseCallback;

        public bool IsSubscribed { get; }
        public string Price { get; }

        public SubscriptionData()
        {
            IsSubscribed = true;
            Price = "0";
        }

        public SubscriptionData(bool isBought, string price, Action<Action> purchaseCallback)
        {
            IsSubscribed = isBought;
            Price = price;
            _purchaseCallback = purchaseCallback ?? throw new ArgumentNullException(nameof(purchaseCallback));
        }

        public void TryPurchaseSubscription(Action onPurchaseCallback)
        {
            _purchaseCallback.Invoke(onPurchaseCallback);
        }
    }

    public interface ISubscriptionData
    {
        public bool IsSubscribed { get; }
        public string Price { get; }

        public void TryPurchaseSubscription(Action onPurchaseCallback);
    }
}
