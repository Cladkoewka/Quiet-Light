using _Project.CodeBase.Services.Audio;
using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class CutTreeState : BasePlayerState
    {
        private readonly InteractionTrigger _interactionTrigger;
        private readonly AudioManager _audioManager;

        public CutTreeState(Player player, PlayerController playerController, Animator animator,
            InteractionTrigger interactionTrigger, AudioManager audioManager)
            : base(player,playerController, animator)
        {
            _interactionTrigger = interactionTrigger;
            _audioManager = audioManager;
        }

        public override void OnEnter()
        {
            Animator.CrossFade(CutTree, CrossFadeDuration);
            _audioManager.SetCutAxeSound(true);
            Player.RestartCutTreeTimer();
            PlayerController.WatchTo(_interactionTrigger.ActiveTree().transform.position);
            _interactionTrigger.ActiveTree().StartCut();
            _interactionTrigger.ResetActiveTree();
        }

        public override void OnExit()
        {
            _interactionTrigger.ActiveInteractable.Interact();
            _audioManager.SetCutAxeSound(false);
        }

    }
}