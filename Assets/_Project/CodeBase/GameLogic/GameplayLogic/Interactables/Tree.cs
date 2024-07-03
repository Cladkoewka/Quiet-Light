using System.Collections;
using _Project.CodeBase.Services.Audio;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameLogic.GameplayLogic.Interactables
{
    public class Tree : MonoBehaviour, IInteractable
    {
        private const string CutTrigger = "Cut";
        
        [Header("Links")]
        [SerializeField] private GameObject _chuckPrefab;
        [SerializeField] private GameObject _transformationEffect;
        [Header("Self Links")]
        [SerializeField] private GameObject _canInteractCircle;
        [SerializeField] private GameObject _cutChuck;
        [SerializeField] private Transform _chuckSpawnPosition;
        [SerializeField] private Animator _animator;
        [Header("Parameters")]
        [SerializeField] private float _lifeTimeAfterCut;
        [SerializeField] private float _transformationPrevarm = 0.1f;
        
        public bool IsCutted { get; private set; }


        private DiContainer _diContainer;
        private AudioManager _audioManager;

        [Inject]
        public void Init(DiContainer diContainer, AudioManager audioManager)
        {
            _diContainer = diContainer;
            _audioManager = audioManager;
        }
        private void Awake()
        {
            ShowInteractable(false);
        }

        public void ShowInteractable(bool value)
        {
            bool isInteractable = value && !IsCutted;
            _canInteractCircle?.SetActive(isInteractable);
        }

        public void Interact() => 
            Cut();

        public void StartCut()
        {
            IsCutted = true;
            _canInteractCircle.SetActive(false);
        }

        private void Cut()
        {
            StartCoroutine(CutCoroutine());
        }

        private IEnumerator CutCoroutine()
        {
            _audioManager.PlayCutTreeSound();
            _animator.SetTrigger(CutTrigger);
            _cutChuck.transform.parent = null;
            yield return new WaitForSeconds(_lifeTimeAfterCut);
            Instantiate(_transformationEffect, _chuckSpawnPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(_transformationPrevarm);
            _diContainer.InstantiatePrefab(_chuckPrefab, _chuckSpawnPosition.position, Quaternion.identity, null);
            Destroy(gameObject);
        }
    }
}