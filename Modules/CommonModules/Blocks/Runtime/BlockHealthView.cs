using Agava.AdditionalPredefinedMethods;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Blocks
{
    public class BlockHealthView : IBlockHealthView, IGameLoop
    {
        private const float TimeToDisable = 1f;

        private readonly Transform _root;
        private readonly Image _image;

        private float _currentTimeToDisable;

        public BlockHealthView(Transform root, Image image)
        {
            _image = image;
            _root = root;
        }

        public void Update(float deltaTime)
        {
            if (_root.gameObject.activeSelf == false)
                return;

            _currentTimeToDisable += deltaTime;

            if (_currentTimeToDisable >= TimeToDisable == false)
                return;

            _currentTimeToDisable = 0f;
            Disable();
        }

        public void Render(int maxHealth, int currentHealth, Vector3Int blockPosition, Vector3 blockSize, IBlockDamageSource blockDamageSource = null)
        {
            _root.gameObject.SetActive(true);
            _root.position = new Vector3(blockPosition.x + .5f, blockPosition.y + 1f, blockPosition.z + .5f);
            _image.fillAmount = (float)currentHealth / maxHealth;

            if (blockDamageSource != null)
                Object.Instantiate(blockDamageSource.Effect, blockPosition + new Vector3(.5f, .5f, .5f), blockDamageSource.Effect.transform.rotation).Play();

            _currentTimeToDisable = 0f;
        }

        public void Disable()
        {
            _root.gameObject.SetActive(false);
        }
    }
}
