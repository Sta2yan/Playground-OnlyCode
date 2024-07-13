using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Shop
{
    internal class SelectedProduct : MonoBehaviour, IPointerClickHandler, IShopInput
    {
        [SerializeField] private SelectedProductView _selectedProductView;

        private IProduct _previousProduct;

        public event Action<IProduct> Changed;

        public IProduct Value { get; private set; }
        public IProduct PreviousValue { get; private set; }

        public void RenderUnavailable()
        {
            _selectedProductView.RenderUnavailable();
        }

        public void Select(IProduct product)
        {
            Value = product;
            _selectedProductView.Render(Value);

            Changed?.Invoke(Value);

            PreviousValue = Value;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ProductView productView))
                Select(productView.Product);

            //if (_previousProduct == productView.Product)
            //    print("1");

        }
    }
}
