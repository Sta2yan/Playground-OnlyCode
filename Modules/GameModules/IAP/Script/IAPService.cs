using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Plauground.IAp
{
    public static class IAPService
    {
        //ID symbols are only a-z, ., _!
        public const string NewYearEventID = "playground.newyear_event";
        public const string SubscriptionID = "playground.subscription";
        //public const string Currency50ID = "playground.currency_50";

        private static readonly IReadOnlyList<string> ConsumableList = new List<string>()
        {
            //Currency50ID,
        };

        public static bool IsConsumableID(string id)
        {
            return ConsumableList.Any(consumableID => string.Equals(id, consumableID, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public enum IApType
    {
        NewYearEvent,
        Subscription,
        //Currency50,
    }
}
