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
            _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);
        }

        public bool IsMoving() => 
            _inputService.MoveInput.magnitude > MovementTreshold;

        public bool IsInteracting() => 
            _inputService.IsInterractButtonDown();
    }
}