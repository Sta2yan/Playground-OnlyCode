using UnityEngine;

namespace Agava.Shop
{
    public class ShopOpenRedDot : MonoBehaviour
    {
        [SerializeField] private Shop _shop;

        private void Update()
        {
            if(_shop.Opened)
                gameObject.SetActive(false);
        }
    }
}
