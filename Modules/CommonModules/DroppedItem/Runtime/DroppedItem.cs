using System.Collections;
using UnityEngine;

namespace Agava.DroppedItems
{
    public class DroppedItem : MonoBehaviour
    {
        [SerializeField] private string _pickUpLayerName;
        [SerializeField, Min(0f)] private float _enablePickUpTime = 1f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;

        private bool _isThrown;
        private Coroutine _coroutine;

        public string Id { get; private set; }
        
        public void Initialize(string id, Mesh mesh, Material material)
        {
            Id = id;
            _meshFilter.mesh = mesh;
            _meshRenderer.material = material;
            
            StartCoroutine(EnablePickUpAfter(_enablePickUpTime));
        }

        public void Push(Vector3 direction, float force)
        {
            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);

            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(EnableThrownAfter(1));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isThrown == false) 
                return;
            
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _isThrown = false;
        }

        private IEnumerator EnableThrownAfter(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isThrown = true;
        }

        private IEnumerator EnablePickUpAfter(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            gameObject.layer = LayerMask.NameToLayer(_pickUpLayerName);
        }
    }
}
