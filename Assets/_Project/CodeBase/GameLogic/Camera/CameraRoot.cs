using System;
using _Project.CodeBase.GameLogic.PlayerLogic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.CodeBase.GameLogic.Camera
{
    public class CameraRoot : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _followTransform;
        [SerializeField] private float _moveSmoothSpeed = 0.5f;
        [SerializeField] private float _rotationSmoothSpeed = 0.5f;

        [Header("Gameplay Camera Transform")]
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private Vector3 _cameraRotation;

        public Quaternion CameraRotation => _cameraTransform.rotation;

        
        [Inject]
        public void Init(Player player)
        {
            _followTransform = player.transform;
        }

        private void Awake() => 
            SetCameraGameplayTransform();

        private void LateUpdate() =>
            Move();

        private void Move()
        {
            transform.position = Vector3.Lerp(transform.position, _followTransform.position, Time.deltaTime * _moveSmoothSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, _followTransform.rotation, Time.deltaTime * _rotationSmoothSpeed);
        }

        public void SetCameraGameplayTransform()
        {
            _cameraTransform.localPosition = _cameraOffset;
            _cameraTransform.localRotation = Quaternion.Euler(_cameraRotation);
        }
        
        public void SetCameraTransform(Vector3 position, Vector3 rotation)
        {
            _cameraTransform.localPosition = position;
            _cameraTransform.localRotation = Quaternion.Euler(rotation);
        }
    }
}