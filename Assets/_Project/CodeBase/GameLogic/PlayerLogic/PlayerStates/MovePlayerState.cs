using _Project.CodeBase.Services.Audio;
using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class MovePlayerState : BasePlayerState
    {
        private readonly AudioManager _audioManager;

        public MovePlayerState(Player player, PlayerController playerController, Animator animator,
            AudioManager audioManager) : base(player, playerController, animator)
        {
            _audioManager = audioManager;
        }

        public override void OnEnter()
        {
           Animator.CrossFade(MoveHash, CrossFadeDuration);
           _audioManager.SetFootstepSound(true);
           Player.SetAxeActive(true);
        }

        public override void OnExit()
        {
            _audioManager.SetFootstepSound(false);
        }

        public override void Update()
        {
            PlayerController.HandleMovement();
            Player.UpdateInteraction();
        }

        public override void FixedUpdate()
        {
            PlayerController.HandleMovement();
        }
    }
}