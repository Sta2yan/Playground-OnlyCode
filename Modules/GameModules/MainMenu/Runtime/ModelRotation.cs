using System;
using UnityEngine;
using Agava.Input;

namespace Agava.Playground3D.MainMenu
{
    public class ModelRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _model;

        private IInput _input;

        public void Initialize(IInput input)
        {
            _input = input;
        }

        private void Update()
        {
            if (_input == null)
                return;

            if (_input.RotateDirection(out Vector2 direction))
            {
                Vector3 rotateDirection = new Vector3(0, -direction.x, 0);
                _model.Rotate(_rotationSpeed * Time.deltaTime * rotateDirection, Space.World);
            }
        }
    }
}
