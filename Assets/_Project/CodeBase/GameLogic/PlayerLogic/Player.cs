using System.Security.Cryptography;
using _Project.CodeBase.Architecture.StateMachine;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.GameLogic.GameplayLogic.Interactables;
using _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates;
using _Project.CodeBase.Services.Audio;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameLogic.PlayerLogic
{
    public class Player : MonoBehaviour
    {

        [Header("Links")]
        [SerializeField] private Animator _animator;
        [SerializeField] private InteractionTrigger _interactionTrigger;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _carryPointTransform;
        [SerializeField] private GameObject _axeGameObject;
        
        
        [Header("Parameters")]
        [SerializeField] private float _cutTreeTime;
        [SerializeField] private float _chopChuckTime;

        
        private StateMachine _stateMachine;
        private AudioManager _audioManager;

        private float _startCutTreeTime;
        private float _startChopChuckTime;

        public Transform CarryPoint => _carryPointTransform;
        public ICarriable Carriable { get; private set; }

        [Inject]
        public void Init(AudioManager audioManager) => 
            _audioManager = audioManager;

        private void Awake()
        {
            SetupStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate() => 
            _stateMachine.FixedUpdate();

        public void RestartCutTreeTimer() => 
            _startCutTreeTime = Time.time;
        public void RestartChopChuckTimer() => 
            _startChopChuckTime = Time.time;

        public void RemoveCarriable()
        {
            IRemovable removable = Carriable as IRemovable;
            removable?.Remove();
            Carriable = null;
        }
        public void UpdateInteraction()
        {
            if (_playerController.IsInteracting())
                _interactionTrigger.ActiveInteractable?.Interact();
        }

        public void SetCarriable(ICarriable carriable) => 
            Carriable = carriable;

        public void SetAxeActive(bool value) => 
            _axeGameObject.SetActive(value);

        private void SetupStateMachine()
        {
            _stateMachine = new StateMachine();

            var moveState = new MovePlayerState(this, _playerController, _animator, _audioManager);
            var carryMoveState = new CarryMovePlayerState(this, _playerController, _animator, _audioManager);
            var idleState = new IdlePlayerState(this,_playerController, _animator);
            var carryIdleState = new CarryIdlePlayerState(this, _playerController, _animator, _interactionTrigger);
            var cutTreeState = new CutTreeState(this,_playerController, _animator, _interactionTrigger, _audioManager);
            var chopChuckState = new ChopChuckState(this, _playerController, _animator, _interactionTrigger, _audioManager);
            
            At(idleState, moveState, new FuncPredicate(() => _playerController.IsMoving()));
            At(moveState, idleState, new FuncPredicate(() => !_playerController.IsMoving()));
            At(moveState, cutTreeState,new FuncPredicate(() => IsCutTree()));
            At(idleState, cutTreeState,new FuncPredicate(() => IsCutTree()));
            At(moveState, chopChuckState,new FuncPredicate(() => IsChopChuck()));
            At(idleState, chopChuckState,new FuncPredicate(() => IsChopChuck()));
            At(idleState, carryIdleState, new FuncPredicate(() => IsShouldCarry()));
            At(moveState, carryMoveState,  new FuncPredicate(() => IsShouldCarry()));
            At(carryIdleState, idleState, new FuncPredicate(() => !IsShouldCarry()));
            At(carryMoveState, moveState, new FuncPredicate(() => !IsShouldCarry()));
            At(carryIdleState, carryMoveState, new FuncPredicate(() => _playerController.IsMoving()));
            At(carryMoveState, carryIdleState, new FuncPredicate(() => !_playerController.IsMoving()));
            
            At(cutTreeState, idleState, new FuncPredicate(() => TreeIsCutted()));
            At(chopChuckState, idleState, new FuncPredicate(() => ChuckIsChopped()));
            
            _stateMachine.SetState(idleState);

        }

        private bool IsShouldCarry() => 
            Carriable != null;

        private bool TreeIsCutted() => 
            Time.time - _startCutTreeTime  >= _cutTreeTime;

        private bool ChuckIsChopped() => 
            Time.time - _startChopChuckTime  >= _chopChuckTime;

        private bool IsCutTree() => 
            _interactionTrigger.ActiveTree() != null && _playerController.IsInteracting();
        private bool IsChopChuck() => 
            _interactionTrigger.ActiveBench() != null &&
            _interactionTrigger.ActiveBench().CanChopChuck() && 
            _playerController.IsInteracting();

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

        
    }
}