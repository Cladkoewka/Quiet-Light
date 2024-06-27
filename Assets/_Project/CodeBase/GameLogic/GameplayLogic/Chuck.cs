using UnityEngine;

namespace _Project.CodeBase.GameLogic.GameplayLogic
{
    public class Chuck : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _canInteractCircle;

        private bool _isTaken;
        private void Awake()
        {
            ShowInteractable(false);
        }

        public void ShowInteractable(bool value)
        {
            _canInteractCircle.SetActive(value);
        }

        public void Interact()
        {
            if (_isTaken)
                Drop();
            else
                Take();
        }

        private void Take()
        {
            _isTaken = true;
        }

        private void Drop()
        {
            _isTaken = false;
        }
    }
}