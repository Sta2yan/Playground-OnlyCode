using System;
using System.Collections;
using System.Threading.Tasks;
//using Assets.Source.AnalitycSystem.Scripts;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace Assets.Source.Plauground.IAp
{
    public class IAPPurchase : IDetailedStoreListener, IIApPurchase
    {
        private const string Environment = "production";

        private IStoreController _storeController;
        private Action _onPurchaseCallback;
        private readonly bool _isEnabled;
        private readonly IAPEntityList _entityList;
        //private readonly IProgressService _progressService;
        private readonly Action _afterLoadCallback;
        private readonly ICoroutine _coroutine;
        private readonly ConfigurationBuilder _builder;

        public IAPPurchase(IAPEntityList iapEntityList, ICoroutine coroutine/*, IProgressService progressService*/, Action afterLoadCallback, bool enabled = true)
        {
            _builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            _entityList = iapEntityList ?? throw new ArgumentNullException(nameof(iapEntityList));
            //_progressService = progressService ?? throw new ArgumentNullException(nameof(progressService));
            _afterLoadCallback = afterLoadCallback ?? throw new ArgumentNullException(nameof(afterLoadCallback));
            _coroutine = coroutine ?? throw new ArgumentNullException(nameof(coroutine));
            _isEnabled = enabled;

            foreach (var entity in _entityList.EntityList)
            {
                _builder.AddProduct(entity.Id, ProductType.NonConsumable);
            }
        }

        public void InitializePurchase()
        {
            //if (_progressService.GameData.AllUnlockModeActivated)
            //{
            //    _entityList.EntityList.ForEach(entity => entity.SetBought());
            //    _afterLoadCallback.Invoke();
            //    return;
            //}

            if (_isEnabled)
            {
                _coroutine.StartCoroutine(WaitForUnityService());

                IEnumerator WaitForUnityService()
                {
                    Task task = ConstructUnityService();
                    yield return new WaitUntil(() => task.IsCompleted);
                    UnityPurchasing.Initialize(this, _builder);
                }
                return;
            }

            //_progressService.SetSubscriptionData(new SubscriptionData(false, "$0.99",
            //    onPurchaseCallback => PurchaseProduct(IApType.Subscription, onPurchaseCallback)));
            _entityList.EntityList.ForEach(entity => entity.SetPrice("0.99$"));
            _afterLoadCallback.Invoke();
        }

        public void PurchaseProduct(IApType iapType, Action onPurchaseCallback)
        {
            _onPurchaseCallback = onPurchaseCallback ?? throw new ArgumentNullException(nameof(onPurchaseCallback));

            if (_isEnabled == false)
            {
                //_progressService.SetSubscriptionData(new SubscriptionData());
                _onPurchaseCallback.Invoke();
                return;
            }

            var productID = _entityList.GetIDByType(iapType);
            _storeController.InitiatePurchase(productID);
        }

        private async Task ConstructUnityService()
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName(Environment);
                await UnityServices.InitializeAsync(options);
            }
            catch (Exception exception)
            {
                Debug.LogError("Failed to construct services with exception: " + exception.Message);
            }
        }

#region IApCallbacks
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            SetProductsBought();
            _afterLoadCallback.Invoke();
            Debug.Log("Purchase initialized!");
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _afterLoadCallback.Invoke();
            Debug.LogError(error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            _afterLoadCallback.Invoke();
            Debug.LogError(error + " " + message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            //AnalyticEvents.OnIApBought(purchaseEvent.purchasedProduct.definition.id);

            var entity = _entityList.GetEntityByID(purchaseEvent.purchasedProduct.definition.id);

            if (IAPService.IsConsumableID(purchaseEvent.purchasedProduct.definition.id) == false)
            {
                entity.SetBought();
            }

            //if (purchaseEvent.purchasedProduct.definition.id == IAPService.SubscriptionID)
            //{
            //    _progressService.SetSubscriptionData(new SubscriptionData());
            //}

            _onPurchaseCallback.Invoke();

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => Debug.LogError(product.definition.id + " " + failureReason);

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError(product.definition.id + " " + failureDescription.message);
        }

        private void SetProductsBought()
        {
            foreach (var entity in _entityList.EntityList)
            {
                var product = _storeController.products.WithID(entity.Id);

                if (product == null)
                {
                    continue;
                }

                TryUpdateSubscription(entity, product);

                if (product.hasReceipt == false || IAPService.IsConsumableID(product.definition.id))
                {
                    entity.SetPrice(product.metadata.localizedPriceString);
                    continue;
                }

                entity.SetBought();
            }
        }

        private void TryUpdateSubscription(IAPEntity entity, Product product)
        {
            if (entity.Id != IAPService.SubscriptionID)
            {
                return;
            }

            if (product.hasReceipt == false)
            {
                //_progressService.SetSubscriptionData(new SubscriptionData(false, product.metadata.localizedPriceString,
                //    onPurchaseCallback => PurchaseProduct(IApType.Subscription, onPurchaseCallback)));
                return;
            }

            var subscriptionManager = new SubscriptionManager(product, null);
            var subscriptionInfo = subscriptionManager.getSubscriptionInfo();
            var subscriptionData = subscriptionInfo.isSubscribed() == Result.True ? new SubscriptionData()
                : new SubscriptionData(false, product.metadata.localizedPriceString,
                    onPurchaseCallback => PurchaseProduct(IApType.Subscription, onPurchaseCallback));

            //_progressService.SetSubscriptionData(subscriptionData);
        }

        #endregion
    }

    public interface IIApPurchase
    {
        public void InitializePurchase();
        public void PurchaseProduct(IApType iapType, Action onPurchaseCallback);
    }
    
}
