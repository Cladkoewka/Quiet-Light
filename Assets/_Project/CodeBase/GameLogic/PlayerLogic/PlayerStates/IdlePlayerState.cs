using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class IdlePlayerState : BasePlayerState
    {
        public IdlePlayerState(PlayerController playerController, Animator animator) : base(playerController, animator) { }
        
        public override void OnEnter()
        {
            Animator.CrossFade(IdleHash, CrossFadeDuration);
        }

    }
}