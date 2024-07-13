using System;
using UnityEngine;
using Agava.Playground3D.CompositeRoot;
using System.Collections.Generic;
using Assets.Source.Plauground.IAp;
using Agava.Playground3D.MainMenu;

namespace Agava.Playground3D.CompositeRoot
{
    public class IAPRoot : CompositeRoot, ICoroutine
    {
        [SerializeField] private IAPEntityList _entityList;

        private List<IAPPurchasePortal> _iAPPurchasePortals = new List<IAPPurchasePortal>();
        //IProgressService progressService;

        public IIApEntityList EntityList => _entityList;
        public IIApPurchase IApPurchase { get; private set; }

        public override void Compose()
        {
            //_entityList = new IAPEntityList();
            IApPurchase = new IAPPurchase(_entityList, this/*, progressService*/, afterLoadCallback, false);
            IApPurchase.InitializePurchase();
        }

        private void OnBuyClicked(IApType iAPType, Action onPurchaseCallback)
        {
            IApPurchase.PurchaseProduct(iAPType, onPurchaseCallback);
        }

        private void OnDisable()
        {
            foreach (var iAPPurchaseButton in _iAPPurchasePortals)
            {
                iAPPurchaseButton.BuyClicked -= OnBuyClicked;
            }
        }

        private Action afterLoadCallback = () =>
        {
            print("afterLoadCallback");
        };

        internal void InitializePortalPurchase(LevelGatePortalContainer[] portalContainers)
        {
            foreach(var portal in portalContainers)
            {
                if(portal.TryGetComponent<IAPPurchasePortal>(out IAPPurchasePortal iAPPurchasePortal))
                {
                    _iAPPurchasePortals.Add(iAPPurchasePortal);
                    iAPPurchasePortal.BuyClicked += OnBuyClicked;
                    string ID = _entityList.GetIDByType(iAPPurchasePortal.IApType);
                    IIApEntity IiApEntity = _entityList.GetEntityByID(ID);
                    iAPPurchasePortal.Init(IiApEntity);
                }

            }
        }
    }
}
