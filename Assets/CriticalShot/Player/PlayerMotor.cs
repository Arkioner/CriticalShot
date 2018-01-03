using System;
using UnityEngine;

namespace CriticalShot.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _panRotation = Vector3.zero;
        private float _tiltRotation;
        private float _currentTiltRotation;
        private Vector3 _thrusterForce = Vector3.zero;

        [SerializeField] private float _tiltCameraRotationLimit = 89f;
        
        private Rigidbody _rigidbody;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            PerformMovement();
            PerformPanRotation();
            PerformTiltRotation();
        }

        private void PerformPanRotation()
        {
            if (_panRotation != Vector3.zero)
            {
                _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_panRotation));
            }
        }
        
        private void PerformTiltRotation()
        {
            if (_camera == null)
            {
                Debug.LogError("No camera is set for player motor");
                return;
            }
            if (Math.Abs(_tiltRotation) > 0.01f)
            {
                _currentTiltRotation += _tiltRotation;
                _currentTiltRotation = Mathf.Clamp(_currentTiltRotation, -_tiltCameraRotationLimit, _tiltCameraRotationLimit);
                _camera.transform.localEulerAngles = new Vector3(_currentTiltRotation, 0f, 0f);
            }
        }

        private void PerformMovement()
        {
            if (_velocity != Vector3.zero)
            {
                _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
            }

            if (_thrusterForce != Vector3.zero)
            {
                _rigidbody.AddForce(_thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }

        public void Move(Vector3 velocity)
        {
            _velocity = velocity;
        }

        public void PanRotate(Vector3 panRotation)
        {
            _panRotation = panRotation;
        }

        public void TiltRotate(float tiltRotation)
        {
            _tiltRotation = tiltRotation;
        }

        public void ThrusterForce(Vector3 thrusterForce)
        {
            _thrusterForce = thrusterForce;
        }
    }
}