using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Agava.Playground3D.CoffeeBreak
{
    public class CoffeeBreakLoader : MonoBehaviour
    {
        [SerializeField] private Milk _milkObjectOnScene;

        private CoffeeBreak _coffeeBreak;
        private Milk _milk;

        private void Start()
        {
#if YANDEX_GAMES
            Destroy(_milkObjectOnScene.gameObject);
            StartCoroutine(LoadCoffeeBreak());
#endif
        }

        private IEnumerator LoadCoffeeBreak()
        {
            var loadOperation = Addressables.LoadAssetAsync<Milk>("Milk");
            yield return loadOperation;

            _milk = Instantiate(loadOperation.Result, _coffeeBreak.transform);
            _coffeeBreak.Initialize(_milk);
        }
    }
}
