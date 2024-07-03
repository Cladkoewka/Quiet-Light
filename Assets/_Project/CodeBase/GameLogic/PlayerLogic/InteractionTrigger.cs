using System;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.GameLogic.GameplayLogic.Fire;
using _Project.CodeBase.GameLogic.GameplayLogic.Interactables;
using UnityEngine;
using Tree = _Project.CodeBase.GameLogic.GameplayLogic.Interactables.Tree;

namespace _Project.CodeBase.GameLogic.PlayerLogic
{
    public class InteractionTrigger : MonoBehaviour
    {
        public IInteractable ActiveInteractable { get; private set; }

        private Chuck _chuck;
        private Bench _bench;
        private Firewood _firewood;
        private Tree _tree;
        private Campfire _campfire;
        
        
        private void OnTriggerEnter(Collider other)
        {
            
            var otherInteractable = other.GetComponent<IInteractable>();
            if (otherInteractable != null) 
                UpdateActiveInteractable(otherInteractable);
        }

        private void OnTriggerStay(Collider other)
        {
            if (ActiveInteractable == null)
            {
                var otherInteractable = other.GetComponent<IInteractable>();
                if(otherInteractable != null)
                    ActiveInteractable = otherInteractable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var otherInteractable = other.GetComponent<IInteractable>();
            if (otherInteractable != null) 
                RemoveInteractable(otherInteractable);
        }

        public Tree ActiveTree() => _tree;
        public Chuck ActiveChuck() => _chuck;
        public Bench ActiveBench() => _bench;
        public Firewood ActiveFirewood() => _firewood;
        public Campfire ActiveCampfire() => _campfire;
        
        
        private void UpdateActiveInteractable(IInteractable otherInteractable)
        {
            TryAddCast(otherInteractable);
            ActiveInteractable = otherInteractable;
            ActiveInteractable.ShowInteractable(true);
        }

        private void RemoveInteractable(IInteractable otherInteractable)
        {
            otherInteractable?.ShowInteractable(false);
            if (otherInteractable == ActiveInteractable) 
                ActiveInteractable = null;
            TryRemoveCast(otherInteractable);
        }

        private void TryRemoveCast(IInteractable otherInteractable)
        {
            Tree tree = otherInteractable as Tree;
            if (_tree == tree)
                _tree = null;
            
            Chuck chuck = otherInteractable as Chuck;
            if (_chuck == chuck)
                _chuck = null;
            
            Bench bench = otherInteractable as Bench;
            if (_bench == bench)
                _bench = null;
            
            Firewood firewood = otherInteractable as Firewood;
            if (_firewood == firewood)
                _firewood = null;
            
            Campfire campfire = otherInteractable as Campfire;
            if (_campfire == campfire)
                _campfire = null;
        }

        private void TryAddCast(IInteractable otherInteractable)
        {
            Tree tree = otherInteractable as Tree;
            if (tree != null)
                _tree = tree;
            
            Chuck chuck = otherInteractable as Chuck;
            if (chuck != null)
                _chuck = chuck;
            
            Bench bench = otherInteractable as Bench;
            if (bench != null)
                _bench = bench;
            
            Firewood firewood = otherInteractable as Firewood;
            if (firewood != null)
                _firewood = firewood;
            
            Campfire campfire = otherInteractable as Campfire;
            if (campfire != null)
                _campfire = campfire;
        }

    }
}