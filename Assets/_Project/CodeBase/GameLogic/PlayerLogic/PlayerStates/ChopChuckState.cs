using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class ChopChuckState : BasePlayerState
    {
        private readonly InteractionTrigger _interactionTrigger;

        public ChopChuckState(Player player, PlayerController playerController, Animator animator,
            InteractionTrigger interactionTrigger)
            : base(player, playerController, animator)
        {
            _interactionTrigger = interactionTrigger;
        }
        
        public override void OnEnter()
        {
            Animator.CrossFade(ChopChuck, CrossFadeDuration);
            Player.RestartChopChuckTimer();
            PlayerController.WatchTo(_interactionTrigger.ActiveBench().transform.position);
        }

        public override void OnExit()
        {
            _interactionTrigger.ActiveInteractable.Interact();
        }

        public override void FixedUpdate() { }
    }
}