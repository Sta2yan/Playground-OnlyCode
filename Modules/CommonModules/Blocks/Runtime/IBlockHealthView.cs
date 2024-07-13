using UnityEngine;

namespace Agava.Blocks
{
    public interface IBlockHealthView
    {
        void Render(int maxHealth, int currentHealth, Vector3Int blockPosition, Vector3 blockSize, IBlockDamageSource blockDamageSource=null);
        void Disable();
    }
}
