using System.Collections;
using _Project.CodeBase.GameLogic.PlayerLogic;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameLogic.GameplayLogic
{
    public class Bench : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _canInteractCircle;
        [SerializeField] private Transform _placeChuckPoint;
        [SerializeField] private GameObject _firewoofPrefab;
        [SerializeField] private GameObject _transformationEffect;
        [SerializeField] private float _transformationPrevarm = 0.1f;

        private InteractionTrigger _interactionTrigger;
        
        public bool IsEngaged { get; private set; }
        
        private IInteractable _placedInteractable;
        private DiContainer _diContainer;

        [Inject]
        public void Init(InteractionTrigger interactionTrigger, DiContainer diContainer)
        {
            _interactionTrigger = interactionTrigger;
            _diContainer = diContainer;
        }
        private void Awake()
        {
            ShowInteractable(false);
        }
        public void ShowInteractable(bool value)
        {
            bool isInteractable = value && (CanPlaceChuck() || CanChopChuck() || CanTakeFirewood());
            _canInteractCircle.SetActive(isInteractable);
        }

        public void Interact()
        {
            if (CanPlaceChuck())
                PlaceChuck();
            else if (CanChopChuck())
                ChopChuck();
            else if (CanTakeFirewood())
                TakeFirewood();
        }

        private void ChopChuck() => 
            StartCoroutine(ChuckTransformation());

        private void PlaceChuck()
        {
            IsEngaged = true;
            _interactionTrigger.ActiveChuck().Place(_placeChuckPoint);
            _placedInteractable = _interactionTrigger.ActiveChuck();
        }

        private void TakeFirewood()
        {
            IsEngaged = false;
            ShowInteractable(false);
            Firewood firewood = _placedInteractable as Firewood;
            firewood.Take();
        }

        public bool CanTakeFirewood() => 
            IsEngaged && _placedInteractable is Firewood;

        public bool CanChopChuck() => 
            IsEngaged && _placedInteractable is Chuck;

        public bool CanPlaceChuck() =>
            IsCarryingChuck() && !IsEngaged;

        private bool IsCarryingChuck() => 
            _interactionTrigger.ActiveChuck() != null && _interactionTrigger.ActiveChuck().IsTaken;

        private IEnumerator ChuckTransformation()
        {
            Instantiate(_transformationEffect, _placeChuckPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(_transformationPrevarm);
            TransformChuckToFirewood();
        }

        private void TransformChuckToFirewood()
        {
            DeleteChuck();

            GameObject firewoodGameObject = _diContainer.InstantiatePrefab(_firewoofPrefab, _placeChuckPoint.position,
                Quaternion.identity, null);
            Firewood firewood = firewoodGameObject.GetComponent<Firewood>();
            _placedInteractable = firewood;
            firewood.Place();
        }

        private void DeleteChuck()
        {
            Chuck transformedChuck = _placedInteractable as Chuck;
            Destroy(transformedChuck.gameObject);
        }

        
    }
}