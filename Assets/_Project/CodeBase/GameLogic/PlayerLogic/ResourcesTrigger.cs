using System;
using UnityEngine;
using Tree = _Project.CodeBase.GameLogic.GameplayLogic.Tree;

namespace _Project.CodeBase.GameLogic.PlayerLogic
{
    public class ResourcesTrigger : MonoBehaviour
    {
        public Tree ActiveTree { get; private set; }


        private void OnTriggerEnter(Collider other)
        {
            var otherTree = other.GetComponent<Tree>();
            
            if (otherTree != null) 
                ActiveTree = otherTree;
                
        }
        
        private void OnTriggerExit(Collider other)
        {
            var otherTree = other.GetComponent<Tree>();

            if (otherTree != null)
            {
                if (otherTree == ActiveTree)
                    ActiveTree = null;
            }
                
        }

    }
}