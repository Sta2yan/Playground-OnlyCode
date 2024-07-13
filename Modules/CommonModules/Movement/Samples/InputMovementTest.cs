using Agava.AdditionalMathValues;
using UnityEngine;

namespace Agava.Movement.Sample
{
    public class InputMovementTest : MonoBehaviour
    {
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private CharacterMovementAdapter _characterMovementAdapter;

        private ICameraMovement _cameraMovementInterface;
        private IDisplacementObject _displacementObjectInterface;
        private float _horizontal;
        private float _vertical;
        private bool _jump;
        
        private void Awake()
        {
            _cameraMovementInterface = _cameraMovement;
            _displacementObjectInterface = _characterMovementAdapter;
            
            var firstPerson = new CameraDefinitionState(0, 15, 60, 65, new FloatRange(-70, 80),
                new Vector3(0, .75f, .3f), new Vector3(0, 0, 0));
            
            var thirdPerson = new CameraDefinitionState(7, 15, 60, 65, new FloatRange(-70, 80),
                new Vector3(0, .7f, 0), new Vector3(1, 0 ,0));
            
            _cameraMovement.Initialize(firstPerson, thirdPerson, thirdPerson);
        }

        private void Update()
        {
            _cameraMovementInterface.ChangeRotation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            
            var scroll = Input.GetAxis("Mouse ScrollWheel");
    
            if (scroll > 0f)
                _cameraMovementInterface.ChangeDistance(-.5f);
            
            if (scroll < 0f) 
                _cameraMovementInterface.ChangeDistance(.5f);
            
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            _horizontal = _horizontal > 0f ? 1 : _horizontal < 0f ? -1 : 0;
            _vertical = _vertical > 0f ? 1 : _vertical < 0f ? -1 : 0;
            
            _horizontal = (int)_horizontal;
            _vertical = (int)_vertical;

            _jump = Input.GetKey(KeyCode.Space);
            
            if (Input.GetKey(KeyCode.LeftShift))
                _displacementObjectInterface.TryEnableSprint(_horizontal, _vertical);
            else
                _displacementObjectInterface.DisableSprint();
        }

        private void FixedUpdate()
        {
            if (_jump)
                _displacementObjectInterface.TryJump();
            
            _displacementObjectInterface.TryMove(_horizontal, _vertical);
        }
    }
}
