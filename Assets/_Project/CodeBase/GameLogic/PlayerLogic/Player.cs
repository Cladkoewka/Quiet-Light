using _Project.CodeBase.Architecture.StateMachine;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates;
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
        private float _startCutTreeTime;
        private float _startChopChuckTime;
        private ICarriable _currentCarriable;

        public Transform CarryPoint => _carryPointTransform;


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

        private void SetupStateMachine()
        {
            _stateMachine = new StateMachine();

            var moveState = new MovePlayerState(this, _playerController, _animator);
            var carryMoveState = new CarryMovePlayerState(this, _playerController, _animator);
            var idleState = new IdlePlayerState(this,_playerController, _animator);
            var carryIdleState = new CarryIdlePlayerState(this, _playerController, _animator, _interactionTrigger);
            var cutTreeState = new CutTreeState(this,_playerController, _animator, _interactionTrigger);
            var chopChuckState = new ChopChuckState(this, _playerController, _animator, _interactionTrigger);
            
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

        public void UpdateInteraction()
        {
            if (_playerController.IsInteracting())
                _interactionTrigger.ActiveInteractable.Interact();
        }

        public void SetCarriable(ICarriable carriable) => 
            _currentCarriable = carriable;

        public void SetAxeActive(bool value) => 
            _axeGameObject.SetActive(value);

        private bool IsShouldCarry() => 
            _currentCarriable != null;

        private bool TreeIsCutted() => 
            Time.time - _startCutTreeTime  >= _cutTreeTime;
        
        private bool ChuckIsChopped() => 
            Time.time - _startChopChuckTime  >= _cutTreeTime;

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