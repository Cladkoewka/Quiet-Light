using _Project.CodeBase.GameLogic.PlayerLogic;
using _Project.CodeBase.Services.Input;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.HUD
{
    public class GameHud : MonoBehaviour
    {
        private const float MinColdAlpha = 0;
        private const float MaxColdAlpha = 0.3f;
        
        [SerializeField] private InteractionHint _interactionHint;
        [SerializeField] private CanvasGroup _coldHUD;

        private InteractionTrigger _interactionTrigger;
        private IInputService _inputService;
        private PlayerCold _playerCold;
        
        [Inject]
        public void Init(IInputService inputService, InteractionTrigger interactionTrigger, PlayerCold playerCold)
        {
            _inputService = inputService;
            _interactionTrigger = interactionTrigger;
            _playerCold = playerCold;
            
            HideInteractHint();
            _inputService.OnInteract += HideInteractHint;
            _coldHUD.alpha = 0;
        }

        private void Update()
        {
            UpdateInteractionHint();
            UpdateColdHud();
        }

        private void UpdateColdHud()
        {
            var multiplier = 1 - _playerCold.RemainingLifeMultiplier;
            _coldHUD.alpha = Mathf.Lerp(MinColdAlpha, MaxColdAlpha, multiplier);
        }

        private void UpdateInteractionHint()
        {
            if (_interactionTrigger.ActiveTree() != null)
                TreeHints();
            else if (_interactionTrigger.ActiveBench() != null)
                BenchHints();
            else if (_interactionTrigger.ActiveCampfire() != null)
                CampfireHints();
            else if (_interactionTrigger.ActiveChuck() != null)
                ChuckHints();
            else if (_interactionTrigger.ActiveFirewood() != null)
                FirewoodHints();
            else
                SetInteractionHintActive(false);
        }

        private void CampfireHints()
        {
            if (_interactionTrigger.ActiveFirewood() != null && _interactionTrigger.ActiveFirewood().IsTaken)
            {
                SetInteractionHintActive(true);
                _interactionHint.SetHintText("To Add Firewood");
            }
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