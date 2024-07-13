using UnityEngine;

namespace Agava.Blocks
{
    public class BlockDamageSource : MonoBehaviour, IBlockDamageSource
    {
        [SerializeField] private ParticleSystem _effect;

        public ParticleSystem Effect => _effect;
    }
}
