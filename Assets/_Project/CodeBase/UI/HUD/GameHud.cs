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
        public void Init(IInputService inputService, InteractionTrigger interactionTrigger)
        {
            _inputService = inputService;
            _interactionTrigger = interactionTrigger;
            
            HideInteractHint();
            _inputService.OnInteract += HideInteractHint;
        }

        private void Update()
        {
            UpdateInteractionHint();
        }

        private void UpdateInteractionHint()
        {
            if (_interactionTrigger.ActiveTree() != null)
                TreeHints();
            else if (_interactionTrigger.ActiveBench() != null)
                BenchHints();
            else if (_interactionTrigger.ActiveChuck() != null)
                ChuckHints();
            else if (_interactionTrigger.ActiveFirewood() != null)
                FirewoodHints();
            else
                SetInteractionHintActive(false);
        }

        private void FirewoodHints()
        {
            SetInteractionHintActive(true);
            if (_interactionTrigger.ActiveFirewood().IsTaken)
            {
                _interactionHint.SetHintText("To Drop Firewood");
            }
            else
            {
                _interactionHint.SetHintText("To Take Firewood");
            }
        }

        private void BenchHints()
        {
            SetInteractionHintActive(true);
            if (_interactionTrigger.ActiveBench().CanPlaceChuck())
                _interactionHint.SetHintText("To Place Chunk");
            else if(_interactionTrigger.ActiveBench().CanChopChuck())
                _interactionHint.SetHintText("To Chop Chunk");
            else if(_interactionTrigger.ActiveBench().CanTakeFirewood())
                _interactionHint.SetHintText("To Take Firewood");
            else
                SetInteractionHintActive(false);
        }

        private void ChuckHints()
        {
            if (_interactionTrigger.ActiveChuck().IsTaken)
            {
                SetInteractionHintActive(true);
                if(_interactionTrigger.ActiveBench() != null)
                    _interactionHint.SetHintText("To Place Chunk");
                else
                    _interactionHint.SetHintText("To Drop Chunk");
            }
            else if (!_interactionTrigger.ActiveChuck().IsPlaced)
            {
                SetInteractionHintActive(true);
                _interactionHint.SetHintText("To Take Chunk");
            }
            else
                SetInteractionHintActive(false);
        }

        private void TreeHints()
        {
            if (!_interactionTrigger.ActiveTree().IsCutted)
            {
                SetInteractionHintActive(true);
                _interactionHint.SetHintText("To Cut Tree");
            }
            else
                SetInteractionHintActive(false);
        }

        private void HideInteractHint() => 
            SetInteractionHintActive(false);

        private void SetInteractionHintActive(bool value) => 
            _interactionHint.gameObject.SetActive(value);

    }
}