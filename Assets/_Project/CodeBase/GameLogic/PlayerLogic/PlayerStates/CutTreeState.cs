using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class CutTreeState : BasePlayerState
    {
        private readonly InteractionTrigger _interactionTrigger;

        public CutTreeState(Player player, PlayerController playerController, Animator animator, InteractionTrigger interactionTrigger)
            : base(player,playerController, animator)
        {
            _interactionTrigger = interactionTrigger;
        }

        public override void OnEnter()
        {
            Animator.CrossFade(CutTree, CrossFadeDuration);
            Player.RestartCutTreeTimer();
            PlayerController.WatchTo(_interactionTrigger.ActiveTree().transform.position);
            _interactionTrigger.ActiveTree().StartCut();
        }

        public override void OnExit()
        {
            _interactionTrigger.ActiveInteractable.Interact();
        }

        public override void FixedUpdate() { }
    }
}