using System;
using Agava.AdditionalMathValues;
using UnityEngine;

namespace Agava.Movement
{
    public class CameraDefinitionState
    {
	    private readonly Vector3 _targetOffset;
	    private readonly Vector3 _defaultCameraOffset;
	    
	    private Vector3 _cameraOffset;

	    public CameraDefinitionState(float distanceToTarget, float maxDistance, int defaultFieldOfView, int sprintFieldOfView, FloatRange floatRotationRange, Vector3 targetOffset, Vector3 cameraOffset)
	    {
	        DistanceToTarget = distanceToTarget;
	        MaxDistance = distanceToTarget > maxDistance ? distanceToTarget : maxDistance;
	        DefaultFieldOfView = defaultFieldOfView;
	        SprintFieldOfView = sprintFieldOfView;
	        RotationRangeVertical = floatRotationRange;
	        _targetOffset = targetOffset;
	        _cameraOffset = cameraOffset;
	        _defaultCameraOffset = cameraOffset;
	    }
        
        public CameraDefinitionState() : this(10f, 15f, 60, 65, new FloatRange(-70f, 80f), Vector3.zero, Vector3.zero) { }
        
        public float DistanceToTarget { get; private set; }
        public float MaxDistance { get; }
        public FloatRange RotationRangeVertical { get; }
        public int DefaultFieldOfView { get; }
        public int SprintFieldOfView { get; }
        
        public Vector3 CameraPivotPosition(Transform target) 
	        => target.position + target.up * _targetOffset.y + target.forward * _targetOffset.z + target.right * _targetOffset.x;

        public Vector3 CameraPosition(Transform camera) 
	        => camera.parent.position - camera.forward * (DistanceToTarget + _cameraOffset.z) + camera.right * _cameraOffset.x + camera.up * _cameraOffset.y;

        public void ChangeDistance(float distance)
        {
	        if (distance < 0f)
		        throw new ArgumentOutOfRangeException(nameof(distance));
	        
	        DistanceToTarget = distance;
        }

        public void ChangeHorizontalOffset(float value)
        {
	        _cameraOffset.x = value;
        }
        
        public void ChangeVerticalOffset(float value)
        {
	        _cameraOffset.z = value;
        }

        public void ResetOffset()
        {
	        _cameraOffset = _defaultCameraOffset;
        }
    }
}
