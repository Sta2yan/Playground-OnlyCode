using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Combat
{
    internal class HitView : MonoBehaviour
    {
        private readonly List<Material> _materials = new List<Material>();
        
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;
        [SerializeField] private Material _material;

        private void Awake()
        {
            for (int i = 0; i < _meshRenderers.Length; i++) 
                _materials.Add(_meshRenderers[i].material);
        }

        public void Execute()
        {
            _effect.Play();
            StartCoroutine(WhiteEffect());
        }

        private IEnumerator WhiteEffect()
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
                _meshRenderers[i].material = _material;
            
            yield return new WaitForSeconds(.1f);
            
            for (int i = 0; i < _meshRenderers.Length; i++)
                _meshRenderers[i].material = _materials[i];
        }
    }
}