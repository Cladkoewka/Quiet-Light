using UnityEngine;
using Tree = _Project.CodeBase.GameLogic.GameplayLogic.Tree;

namespace _Project.CodeBase.GameLogic.PlayerLogic.PlayerStates
{
    public class ChopTreeState : BasePlayerState
    {
        private readonly ResourcesTrigger _resourcesTrigger;

        public ChopTreeState(PlayerController playerController, Animator animator, ResourcesTrigger resourcesTrigger) :
            base(playerController, animator)
        {
            _resourcesTrigger = resourcesTrigger;
        }

        public override void OnEnter()
        {
            Animator.CrossFade(ChopTree, CrossFadeDuration);
            PlayerController.UpdateChopTreeTimer();
            PlayerController.WatchTo(_resourcesTrigger.ActiveTree.transform.position);
        }

        public override void OnExit()
        {
            _resourcesTrigger.ActiveTree.Cut();
        }

        public override void FixedUpdate()
        {
            
        }
    }
}