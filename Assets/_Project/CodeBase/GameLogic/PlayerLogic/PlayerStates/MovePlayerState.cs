using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class MovePlayerState : BasePlayerState
    {
        public MovePlayerState(Player player, PlayerController playerController, Animator animator) 
            : base(player, playerController, animator) { }

        public override void OnEnter()
        {
           Animator.CrossFade(MoveHash, CrossFadeDuration);
        }

        public override void Update()
        {
            PlayerController.HandleMovement();
            Player.UpdateInteraction();
        }
    }
}