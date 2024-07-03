using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class IdlePlayerState : BasePlayerState
    {
        public IdlePlayerState(Player player, PlayerController playerController, Animator animator) 
            : base(player, playerController, animator) { }
        
        public override void OnEnter()
        {
            Animator.CrossFade(IdleHash, CrossFadeDuration);
        }

        public override void Update()
        {
            Player.UpdateInteraction();
        }
        
        public override void FixedUpdate()
        {
            PlayerController.HandleMovement();
        }
    }
}