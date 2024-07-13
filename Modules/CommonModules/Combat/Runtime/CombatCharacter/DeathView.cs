using UnityEngine;

namespace Agava.Combat
{
    public class DeathView : MonoBehaviour
    {
        private GameObject _deathCharacterTemplate;
        private GameObject _deathCharacterInstance;

        public void Initialize(GameObject deathCharacter)
        {
            _deathCharacterTemplate = deathCharacter;
        }

        public void Render(Vector3 position, Quaternion rotation)
        {
            if (_deathCharacterInstance != null)
                Disable();

            _deathCharacterInstance = Instantiate(_deathCharacterTemplate, position, rotation);
        }

        public void Disable()
        {
            Destroy(_deathCharacterInstance);
        }
    }
}
