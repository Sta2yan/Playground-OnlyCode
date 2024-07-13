using UnityEngine;

namespace Agava.Playground3D.Sandbox.ItemsPanel
{
    public class RedDot : MonoBehaviour
    {
        [SerializeField] private CatalogTabView _catalogTabView;

        private void Update()
        {
            if (_catalogTabView.Selected)
                gameObject.SetActive(false);
        }
    }
}
