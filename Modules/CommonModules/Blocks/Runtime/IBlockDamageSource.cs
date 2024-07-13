using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Blocks
{
    public interface IBlockDamageSource
    {
        public ParticleSystem Effect { get; }
    }
}
