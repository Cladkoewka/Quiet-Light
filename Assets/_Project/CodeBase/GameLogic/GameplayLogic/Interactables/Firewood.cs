using _Project.CodeBase.GameLogic.PlayerLogic;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameLogic.GameplayLogic.Interactables
{
    public class Firewood : MonoBehaviour, IInteractable, ICarriable, IRemovable
    {
        [Header("Links")]
        [SerializeField] private GameObject _canInteractCircle;
        [SerializeField] private Collider _collider;
        [SerializeField] private Collider _triggerCollider;
        
        [Header("Parameters")]
        [SerializeField] private float _carryScaleMultiplier = 0.7f;

        
        public bool IsTaken { get; private set; }
        public bool IsPlaced { get;  private set; }
        
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
            bool isInteractable = value && !IsPlaced && !IsTaken;
            _canInteractCircle.SetActive(isInteractable);
        }

        public void Interact()
        {
            if (IsTaken)
                Drop();
            else 
                Take();
        }
        
        public void Place()
        {
            IsPlaced = true;
            _canInteractCircle.SetActive(false);
            _triggerCollider.enabled = false;
        }
        public void Take()
        {
            IsTaken = true;
            IsPlaced = false;
            _triggerCollider.enabled = true;
            ShowInteractable(false);
            _collider.enabled = false;
            transform.localScale = Vector3.one * _carryScaleMultiplier;
            transform.parent = _player.transform;
            transform.localPosition = _player.CarryPoint.localPosition;
            _player.SetAxeActive(false);
            _player.SetCarriable(this);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        private void Drop()
        {
            IsTaken = false;
            ShowInteractable(true);
            _collider.enabled = true;
            transform.localScale = Vector3.one;
            transform.parent = null;
            transform.position = GroundedPosition();
            _player.SetAxeActive(true);
            _player.SetCarriable(null);
        }
        
        private Vector3 GroundedPosition() => 
            new(transform.position.x, 0, transform.position.z);

        
    }
}