using UnityEngine;

namespace Agava.Tests
{
    [CreateAssetMenu(fileName = "TestConfig", menuName = "Create TestConfig", order = 51)]
    public class TestConfig : ScriptableObject
    {
        [field: SerializeField] public bool MobileInput { get; private set; } = false;
        [field: SerializeField] public bool AllContentUnlocked { get; private set; } = false;
    }
}
