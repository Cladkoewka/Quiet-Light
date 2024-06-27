using _Project.CodeBase.Architecture.StateMachine;
using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public abstract class BasePlayerState : IState
    {
        protected readonly Player Player;
        protected readonly PlayerController PlayerController;
        protected readonly Animator Animator;

        protected static readonly int MoveHash = Animator.StringToHash("Move");
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int ChopTree = Animator.StringToHash("Chop Tree");

        protected const float CrossFadeDuration = 0.1f;

        protected BasePlayerState(Player player, PlayerController playerController, Animator animator)
        {
            Player = player;
            PlayerController = playerController;
            Animator = animator;
        }
        
        public virtual void OnEnter()
        {
            Animator.CrossFade(MoveHash, CrossFadeDuration);
        }

        public virtual void FixedUpdate()
        {
            PlayerController.HandleMovement();
        }

        public virtual void Update() { }

        public virtual void OnExit() { }
    }
}