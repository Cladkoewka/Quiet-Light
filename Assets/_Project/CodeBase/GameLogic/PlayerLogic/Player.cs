using _Project.CodeBase.Architecture.StateMachine;
using _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates;
using _Project.CodeBase.Services.Input;
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
        
        [Header("Parameters")]
        [SerializeField] private float _chopTreeTime;

        
        private StateMachine _stateMachine;
        
        
        private float _startChopTreeTime;
        

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

            var moveState = new MovePlayerState(this, _playerController, _animator);
            var idleState = new IdlePlayerState(this,_playerController, _animator);
            var chopTreeState = new ChopTreeState(this,_playerController, _animator, _interactionTrigger);
            
            At(idleState, moveState, new FuncPredicate(() => _playerController.IsMoving()));
            At(moveState, idleState, new FuncPredicate(() => !_playerController.IsMoving()));
            At(moveState, chopTreeState,new FuncPredicate(() => CanChopTree()));
            At(idleState, chopTreeState,new FuncPredicate(() => CanChopTree()));
            At(chopTreeState, idleState, new FuncPredicate(() => TreeIsChopped()));
            
            _stateMachine.SetState(idleState);

        }

        private bool TreeIsChopped() => 
            Time.time - _startChopTreeTime  >= _chopTreeTime;

        private bool CanChopTree() => 
            _interactionTrigger.ActiveTree() != null && _playerController.IsInteracting();

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}