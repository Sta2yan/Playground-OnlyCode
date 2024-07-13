using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;

namespace Assets.Source.Plauground.IAp
{
    [Serializable]
    public class IAPEntityList : IIApEntityList
    {
        [Preserve] public List<IAPEntity> EntityList = new()
        {
            new IAPEntity(IAPService.NewYearEventID),
            new IAPEntity(IAPService.SubscriptionID),
            //new IAPEntity(IAPService.Currency50ID),
        };

        public string GetIDByType(IApType iapType)
        {
            return iapType switch
            {
                IApType.NewYearEvent => IAPService.NewYearEventID,
                IApType.Subscription => IAPService.SubscriptionID,
                //IApType.Currency50 => IAPService.Currency50ID,
                _ => throw new NotImplementedException(nameof(iapType))
            };
        }

        public IIApEntity GetEntityByID(string entityID)
        {
            var entity = EntityList.FirstOrDefault(entity => entity.Id.ToLower().Equals(entityID.ToLower()));

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity.Id) + " is not in list!");
            }

            return entity;
        }
    }

    public interface IIApEntityList
    {
        public string GetIDByType(IApType iapType);
        public IIApEntity GetEntityByID(string entityID);
    }
}
