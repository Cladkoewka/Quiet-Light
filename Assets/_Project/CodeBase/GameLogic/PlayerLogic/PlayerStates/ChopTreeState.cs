using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class ChopTreeState : BasePlayerState
    {
        private readonly InteractionTrigger _interactionTrigger;

        public ChopTreeState(Player player, PlayerController playerController, Animator animator, InteractionTrigger interactionTrigger)
            : base(player,playerController, animator)
        {
            _interactionTrigger = interactionTrigger;
        }

        public override void OnEnter()
        {
            Animator.CrossFade(ChopTree, CrossFadeDuration);
            Player.UpdateChopTreeTimer();
            Player.WatchTo(_interactionTrigger.ActiveTree().transform.position);
        }

        public override void OnExit()
        {
            _interactionTrigger.ActiveInteractable.Interact();
        }

        public override void FixedUpdate()
        {
            
        }
    }
}