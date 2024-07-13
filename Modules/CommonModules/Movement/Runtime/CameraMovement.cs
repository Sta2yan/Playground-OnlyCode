using System;
using Agava.Save;
using DG.Tweening;
using UnityEngine;

namespace Agava.Movement
{
    public class CameraMovement : MonoBehaviour, ICameraMovement
    {
        private const string SensitivitySaveKey = "SensitivitySaveKey";
        private const float ViewPersonValue = 2f;
        private const float RateOfChangeTurn = 30f;
        private const float BarrierValue = 1f;
        private const float LerpSmooth = 50f;
        private const float SensitivitySmooth = 2f;

        [SerializeField] private Camera _cameraMain;
        [SerializeField] private Transform _targetToFollow;
        [SerializeField] private Sprint _sprint;
        [SerializeField, Range(.1f, 50f)] private float _sensitivity = 1f;

        private Vector2 _targetRotation;
        private Vector2 _startRotation;
        private CameraDefinitionState _firstPersonCameraDefinitionState;
        private CameraDefinitionState _thirdPersonCameraDefinitionState;
        private CameraDefinitionState _currentCameraDefinitionState;

        private int _lastFov;
        private bool _rotationEnabled = true;
        private bool _active = true;

        public Camera CameraMain => _cameraMain;
        public bool FirstPersonPerspective => _currentCameraDefinitionState.DistanceToTarget < ViewPersonValue;
        public float Sensitivity => _sensitivity;

        private void Start()
        {
            ChangeCameraRotation(0f, _targetToFollow.eulerAngles.y);
        }

        private void Update()
        {
            if (_active == false)
                return;
            
            UpdateCameraPosition();
            UpdateCameraPivotPosition();
        }

        private void FixedUpdate()
        {
            if (_active == false)
                return;
            
            if (_targetToFollow.gameObject.activeSelf && _rotationEnabled)
                UpdateRotate();
        }

        public void Initialize(CameraDefinitionState firstPersonCameraDefinitionState, CameraDefinitionState thirdPersonCameraDefinitionState, CameraDefinitionState defaultState = null)
        {
            if (firstPersonCameraDefinitionState == null)
                throw new NullReferenceException(nameof(firstPersonCameraDefinitionState));
            
            if (thirdPersonCameraDefinitionState == null)
                throw new NullReferenceException(nameof(thirdPersonCameraDefinitionState));
            
            ReplaceFirstPersonState(firstPersonCameraDefinitionState);
            ReplaceThirdPersonState(thirdPersonCameraDefinitionState);

            _currentCameraDefinitionState = defaultState ?? thirdPersonCameraDefinitionState;

            _sensitivity = SaveFacade.GetFloat(SensitivitySaveKey, _sensitivity);
        }
        
        public void ChangeRotation(float vertical, float horizontal)
        {
            _targetRotation.x -= _sensitivity / SensitivitySmooth * horizontal;
            _targetRotation.y += _sensitivity / SensitivitySmooth * vertical;
            
            _targetRotation.x = Mathf.Clamp(_targetRotation.x, _currentCameraDefinitionState.RotationRangeVertical.Min, _currentCameraDefinitionState.RotationRangeVertical.Max);
            
            _startRotation.x = Mathf.Lerp(_startRotation.x, _targetRotation.x, RateOfChangeTurn * Time.deltaTime);
            _startRotation.y = Mathf.Lerp(_startRotation.y, _targetRotation.y, RateOfChangeTurn * Time.deltaTime);
        }

        public void ChangeTargetRotation(Vector3 target)
        {
            _targetRotation = target;
        }
        
        public void ChangeDistance(float step)
        {
            var distance = _currentCameraDefinitionState.DistanceToTarget + step;;

            ConfineDistance(ref distance);
            UpdateViewPerson(distance);

            _currentCameraDefinitionState.ChangeDistance(distance);

            void ConfineDistance(ref float distance)
            {
                if (distance is > BarrierValue and < ViewPersonValue)
                    distance = 0f;

                if (distance is > 0f and < BarrierValue) 
                    distance = ViewPersonValue;

                distance = Mathf.Clamp(distance, 0f, _currentCameraDefinitionState.MaxDistance);
            }
        }

        public void ChangeSensitivity(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value));

            _sensitivity = value;
            SaveFacade.SetFloat(SensitivitySaveKey, _sensitivity);
        }

        public void ChangeFieldOfView(int fieldOfView)
        {
            if (fieldOfView <= 0f)
                throw new ArgumentOutOfRangeException(nameof(fieldOfView));

            if (_lastFov == fieldOfView)
                return;
            
            _lastFov = fieldOfView;
                
            _cameraMain.DOComplete();
            _cameraMain.DOFieldOfView(_lastFov, 0.15f);
        }
        
        public void SetActiveRotation(bool value)
        {
            _rotationEnabled = value;
        }

        public void ChangeHorizontalOffset(float value)
        {
            _thirdPersonCameraDefinitionState.ChangeHorizontalOffset(value);
        }

        public void ResetOffset()
        {
            _thirdPersonCameraDefinitionState.ResetOffset();
            _firstPersonCameraDefinitionState.ResetOffset();
        }

        public void Active()
        {
            _active = true;
        }

        public void Disable()
        {
            _active = false;
        }

        private void ReplaceFirstPersonState(CameraDefinitionState cameraDefinitionState)
        {
            _firstPersonCameraDefinitionState = cameraDefinitionState ?? throw new NullReferenceException(nameof(cameraDefinitionState));
        }

        private void ReplaceThirdPersonState(CameraDefinitionState cameraDefinitionState)
        {
            _thirdPersonCameraDefinitionState = cameraDefinitionState ?? throw new NullReferenceException(nameof(cameraDefinitionState));
        }

        private void UpdateRotate()
        {
            var rotation = Quaternion.Euler(new Vector3(_startRotation.x, _startRotation.y, 0f));
            
            var targetRotation = _targetToFollow.root.rotation;
            targetRotation = Quaternion.AngleAxis(0f, transform.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, LerpSmooth * Time.fixedDeltaTime);
            
            _cameraMain.transform.parent.localRotation = Quaternion.FromToRotation(transform.up, Vector3.zero) * rotation;
        }

        private void UpdateCameraPosition()
        {
            _cameraMain.transform.position = _currentCameraDefinitionState.CameraPosition(_cameraMain.transform);
        }

        private void UpdateCameraPivotPosition()
        {
            transform.position = _currentCameraDefinitionState.CameraPivotPosition(_targetToFollow);
        }
        
        private void UpdateViewPerson(float distance)
        {
            _currentCameraDefinitionState = distance < ViewPersonValue ? _firstPersonCameraDefinitionState : _thirdPersonCameraDefinitionState;
        }

        private void ChangeCameraRotation(float targetRotationX, float targetRotationY)
        {
            _targetRotation.x = targetRotationX;
            _targetRotation.y = targetRotationY;
        }
    }
}
