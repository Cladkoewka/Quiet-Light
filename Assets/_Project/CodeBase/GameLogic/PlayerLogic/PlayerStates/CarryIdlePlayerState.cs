using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class CarryIdlePlayerState : BasePlayerState
    {
        private readonly InteractionTrigger _interactionTrigger;

        public CarryIdlePlayerState(Player player, PlayerController playerController, Animator animator, InteractionTrigger interactionTrigger) 
            : base(player, playerController, animator)
        {
            _interactionTrigger = interactionTrigger;
        }

        public override void OnEnter()
        {
            Animator.CrossFade(CarryIdleHash, CrossFadeDuration);
        }

        public override void Update()
        {
            Player.UpdateInteraction();
        }
    }
}