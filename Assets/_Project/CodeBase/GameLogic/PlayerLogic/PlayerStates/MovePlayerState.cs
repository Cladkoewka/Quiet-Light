using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class MovePlayerState : BasePlayerState
    {
        public MovePlayerState(PlayerController playerController, Animator animator) : base(playerController, animator) { }

        public override void OnEnter()
        {
           Animator.CrossFade(MoveHash, CrossFadeDuration);
        }

        public override void Update()
        {
            PlayerController.HandleMovement();
        }
    }
}