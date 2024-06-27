using System;
using System.Collections;
using UnityEngine;

namespace _Project.CodeBase.GameLogic.GameplayLogic
{
    public class Tree : MonoBehaviour, IInteractable
    {
        private const string CutTrigger = "Cut";
        
        [SerializeField] private GameObject _canInteractCircle;
        [SerializeField] private GameObject _cutChuck;
        [SerializeField] private Transform _chuckSpawnPosition;
        [SerializeField] private GameObject _chuckPrefab;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _lifeTimeAfterCut;

        private void Awake()
        {
            ShowInteractable(false);
        }

        public void ShowInteractable(bool value)
        {
            _canInteractCircle.SetActive(value);
        }

        public void Interact() => 
            Cut();

        private void Cut() => 
            StartCoroutine(CutCoroutine());

        private IEnumerator CutCoroutine()
        {
            _animator.SetTrigger(CutTrigger);
            _cutChuck.transform.parent = null;
            yield return new WaitForSeconds(_lifeTimeAfterCut);
            Instantiate(_chuckPrefab, _chuckSpawnPosition.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}