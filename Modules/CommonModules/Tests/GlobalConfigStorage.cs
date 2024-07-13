using UnityEngine;

namespace Agava.Tests
{
    public class GlobalConfigStorage : MonoBehaviour
    {
        [SerializeField] private TestConfig _config;

        public bool MobileInput => _config.MobileInput;
        public bool AllContentUnlocked => _config.AllContentUnlocked;
    }
}
