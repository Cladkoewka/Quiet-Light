using System;
using _Project.CodeBase.Architecture.StateMachine;
using _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates;
using _Project.CodeBase.Services.Input;
using UnityEngine;
using Zenject;
using Tree = _Project.CodeBase.GameLogic.GameplayLogic.Tree;

namespace _Project.CodeBase.GameLogic.PlayerLogic
{
    public class PlayerController : MonoBehaviour
    {
        private const float MovementTreshold = 0.01f;

        [Header("Links")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private ResourcesTrigger _resourcesTrigger;
        
        [Header("Parameters")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotatitonSensitivity;
        [SerializeField] private float _chopTreeTime;

        private IInputService _inputService;
        private StateMachine _stateMachine;
        
        
        private float _startChopTreeTime;


        [Inject]
        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            SetupStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update();
            Debug.Log(_stateMachine.CurrentStateToString());
        }

        private void FixedUpdate() => 
            _stateMachine.FixedUpdate();

        public void WatchTo(Vector3 transformPosition)
        {
            Vector3 direction = transformPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        public void UpdateChopTreeTimer() => 
            _startChopTreeTime = Time.time;

        private void SetupStateMachine()
        {
            _stateMachine = new StateMachine();

            var moveState = new MovePlayerState(this, _animator);
            var idleState = new IdlePlayerState(this, _animator);
            var chopTreeState = new ChopTreeState(this, _animator, _resourcesTrigger);
            
            At(idleState, moveState, new FuncPredicate(() => IsMoving()));
            At(moveState, idleState, new FuncPredicate(() => !IsMoving()));
            At(moveState, chopTreeState,new FuncPredicate(() => CanChopTree()));
            At(chopTreeState, idleState, new FuncPredicate(() => TreeIsChopped()));
            
            _stateMachine.SetState(idleState);

        }

        private bool TreeIsChopped() => 
            Time.time - _startChopTreeTime  >= _chopTreeTime;

        private bool CanChopTree() => 
            _resourcesTrigger.ActiveTree != null && _inputService.IsInterractButtonDown();

        public void HandleMovement()
        {
            transform.Rotate(Vector3.up * _inputService.CameraInput.x * _rotatitonSensitivity, Space.Self);
            
            Vector3 moveDirection = transform.TransformDirection(_inputService.MoveInput);
            _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);
        }

        private bool IsMoving() => 
            _inputService.MoveInput.magnitude > MovementTreshold;

        private void IsInteracting() =>
            _inputService.IsInterractButtonDown();

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}