using System;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.GameLogic.PlayerLogic;
using _Project.CodeBase.Services.Input;
using UnityEngine;
using Zenject;
using Tree = _Project.CodeBase.GameLogic.GameplayLogic.Tree;

namespace _Project.CodeBase.UI.HUD
{
    public class GameHud : MonoBehaviour
    {
        [SerializeField] private InteractionHint _interactionHint;

        private InteractionTrigger _interactionTrigger;
        private IInputService _inputService;
        
        [Inject]
        public void Init(IInputService inputService) => 
            _inputService = inputService;

        public void Construct(InteractionTrigger interactionTrigger)
        {
            _interactionTrigger = interactionTrigger;
            SetInteractionHintActive(false);
            _interactionTrigger.OnInteractableChanged += HandleIteractableChange;
            _inputService.OnInteract += HideInteractHint;
        }

        private void HideInteractHint() => 
            SetInteractionHintActive(false);


        private void HandleIteractableChange(IInteractable interactable)
        {
            if (interactable == null)
                SetInteractionHintActive(false);
            else
            {
                SetInteractionHintActive(true);
                SetInteractionHint(interactable);
            }
        }

        private void SetInteractionHintActive(bool value) => 
            _interactionHint.gameObject.SetActive(value);

        private void SetInteractionHint(IInteractable interactable)
        {
            if (interactable is Tree)
                _interactionHint.SetHintText("To Cut Tree");
            else if (interactable is Chuck)
                _interactionHint.SetHintText("To Take Chunk");
        }
    }
}