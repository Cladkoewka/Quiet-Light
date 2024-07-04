using _Project.CodeBase.GameLogic.PlayerLogic;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameLogic.GameplayLogic.Interactables
{
    public class Chuck : MonoBehaviour, IInteractable, ICarriable
    {
        [SerializeField] private GameObject _canInteractCircle;
        [SerializeField] private Collider _collider;
        [SerializeField] private Collider _triggerCollider;
        [SerializeField] private float _carryScaleMultiplier = 0.7f;

        
        public bool IsTaken { get; private set; }
        public bool IsPlaced { get; set; }
        
        private Player _player;


        [Inject]
        public void Init(Player player)
        {
            _player = player;
        }
        
        private void Awake()
        {
            ShowInteractable(false);
        }

        public void ShowInteractable(bool value)
        {
            bool isInteractable = value && !IsPlaced;
            _canInteractCircle.SetActive(isInteractable);
        }

        public void Interact()
        {
            if (IsTaken)
                Drop();
            else if (!IsPlaced)
                Take();
        }

        public void Place(Transform placeTransform)
        {
            IsTaken = false;
            IsPlaced = true;
            ShowInteractable(false);
            _collider.enabled = false;
            transform.localScale = Vector3.one * _carryScaleMultiplier;
            transform.parent = placeTransform;
            transform.position = placeTransform.position;
            _player.SetCarriable(null);
            _triggerCollider.enabled = false;
        }


        private void Take()
        {
            IsTaken = true;
            ShowInteractable(false);
            _collider.enabled = false;
            transform.localScale = Vector3.one * _carryScaleMultiplier;
            transform.parent = _player.transform;
            transform.localPosition = _player.CarryPoint.localPosition;
            _player.SetCarriable(this);
            
        }

        private void Drop()
        {
            IsTaken = false;
            ShowInteractable(true);
            _collider.enabled = true;
            transform.localScale = Vector3.one;
            transform.parent = null;
            transform.position = GroundedPosition();
            _player.SetCarriable(null);
        }

        private Vector3 GroundedPosition() => 
            new(transform.position.x, 0, transform.position.z);
    }
}