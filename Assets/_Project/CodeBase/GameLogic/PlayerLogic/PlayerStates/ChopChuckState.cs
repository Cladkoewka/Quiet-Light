using _Project.CodeBase.Services.Audio;
using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class ChopChuckState : BasePlayerState
    {
        private readonly InteractionTrigger _interactionTrigger;
        private readonly AudioManager _audioManager;

        public ChopChuckState(Player player, PlayerController playerController, Animator animator,
            InteractionTrigger interactionTrigger, AudioManager audioManager)
            : base(player, playerController, animator)
        {
            _interactionTrigger = interactionTrigger;
            _audioManager = audioManager;
        }
        
        public override void OnEnter()
        {
            Animator.CrossFade(ChopChuck, CrossFadeDuration);
            _audioManager.SetAxeSound(true);
            Player.RestartChopChuckTimer();
            PlayerController.WatchTo(_interactionTrigger.ActiveBench().transform.position);
        }

        public override void OnExit()
        {
            _interactionTrigger.ActiveInteractable.Interact();
            _audioManager.SetAxeSound(false);
        }

    }
}