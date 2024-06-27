using System;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.UI.HUD;
using UnityEngine;
using Zenject;
using Tree = _Project.CodeBase.GameLogic.GameplayLogic.Tree;

namespace _Project.CodeBase.GameLogic.PlayerLogic
{
    public class InteractionTrigger : MonoBehaviour
    {
        public IInteractable ActiveInteractable { get; private set; }

        public event Action<IInteractable> OnInteractableChanged; 
        

        private void OnTriggerEnter(Collider other)
        {
            
            var otherInteractable = other.GetComponent<IInteractable>();
            if (otherInteractable != null) 
                UpdateActiveInteractable(otherInteractable);
        }

        private void OnTriggerExit(Collider other)
        {
            var otherInteractable = other.GetComponent<IInteractable>();
            if (otherInteractable != null) 
                RemoveInteractable(otherInteractable);
        }

        private void UpdateActiveInteractable(IInteractable otherInteractable)
        {
            ActiveInteractable?.ShowInteractable(false);
            ActiveInteractable = otherInteractable;
            ActiveInteractable.ShowInteractable(true);
            OnInteractableChanged?.Invoke(ActiveInteractable);
        }

        private void RemoveInteractable(IInteractable otherInteractable)
        {
            otherInteractable.ShowInteractable(false);
            if (otherInteractable == ActiveInteractable)
            {
                ActiveInteractable = null;
                OnInteractableChanged?.Invoke(null);
            }
        }

        public Tree ActiveTree() => 
            ActiveInteractable as Tree;
        public Chuck ActiveChuck() =>
            ActiveInteractable as Chuck;
    }
}