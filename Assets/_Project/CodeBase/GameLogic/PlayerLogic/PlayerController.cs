using _Project.CodeBase.Services.Input;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameLogic.PlayerLogic
{
    public class PlayerController : MonoBehaviour
    {
        
        private const float MovementTreshold = 0.01f;
        
        [Header("Links")]
        [SerializeField] private CharacterController _characterController;
        
        [Header("Parameters")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotatitonSensitivity;
        
        private IInputService _inputService;
        
        [Inject]
        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void HandleMovement()
        {
            transform.Rotate(Vector3.up * _inputService.CameraInput.x * _rotatitonSensitivity, Space.Self);
            
            Vector3 moveDirection = transform.TransformDirection(_inputService.MoveInput);
            Vector3 moveVector = moveDirection * _moveSpeed * Time.deltaTime;
            moveVector.y = 0;
            _characterController.Move(moveVector);
        }
        public void WatchTo(Vector3 transformPosition)
        {
            Vector3 direction = transformPosition - transform.position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        public bool IsMoving() => 
            _inputService.MoveInput.magnitude > MovementTreshold;

        public bool IsInteracting() => 
            _inputService.IsInterractButtonDown();
    }
}