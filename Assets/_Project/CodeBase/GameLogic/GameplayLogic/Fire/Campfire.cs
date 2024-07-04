using System;
using _Project.CodeBase.GameLogic.GameplayLogic.Interactables;
using _Project.CodeBase.GameLogic.PlayerLogic;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameLogic.GameplayLogic.Fire
{
    public class Campfire : MonoBehaviour, IInteractable
    {
        private const int LevelsCount = 3;
        private const float MinFireParticlesMultiplier = 0.3f;
        private const float MaxFireParticlesMultiplier = 1.3f;
        private const float MinFireLightRadius = 5f;
        private const float MaxFireLightRadius = 30f;

        [Header("Links")]
        [SerializeField] private GameObject _canInteractCircle;
        [SerializeField] private GameObject[] _campfireModels;
        [SerializeField] private ParticleSystem _fireParticles;
        [SerializeField] private Light _fireLight;
        [SerializeField] private FireWarm _fireWarm;

        [Header("Parameters")] 
        [SerializeField] private float _campfireLifetime;
        [SerializeField] private float _campfireFillByFirewood;
        
        public event Action OnFaded;
        
        private Player _player;
        
        private float _remainingTime;
        private int _currentLevel;
        private bool _isFaded;

        [Inject]
        public void Init(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _remainingTime = _campfireLifetime;
            _currentLevel = LevelsCount;
            SetLevel(LevelsCount);
            ShowInteractable(false);
        }

        private void Update()
        {
            if (_remainingTime > 0)
                UpdateCampfire();
            else
                FadeCampfire();
        }

        private void FadeCampfire()
        {
            if (_isFaded)
                return;

            _isFaded = true;
            _fireLight.enabled = false;
            _fireParticles.Stop(true);
            _fireWarm.StopWarm();
            OnFaded?.Invoke();
        }

        public void ShowInteractable(bool value)
        {
            bool isInteractable = value && CanFillCampfire();
            _canInteractCircle.SetActive(isInteractable);
        }

        public void Interact()
        {
            if (CanFillCampfire())
            {
                _player.RemoveCarriable();
                FillCampfire();
                ShowInteractable(false);
            }
        }
        
        private void FillCampfire()
        {
            _remainingTime += _campfireFillByFirewood;
            if (_remainingTime > _campfireLifetime)
                _remainingTime = _campfireLifetime;
        }

        private void UpdateCampfire()
        {
            _remainingTime -= Time.deltaTime;
            UpdateLevel();
            
            float multiplieRemainingTime = _remainingTime / _campfireLifetime;
            UpdateFireScale(multiplieRemainingTime);
            UpdateLight(multiplieRemainingTime);
            _fireWarm.UpdateRadius(multiplieRemainingTime);
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel(_remainingTime);
            if (_currentLevel != newLevel)
            {
                _currentLevel = newLevel;
                SetLevel(_currentLevel);
            }
        }

        private void UpdateLight(float multiplieRemainingTime)
        {
            float lightRange = Mathf.Lerp(MinFireLightRadius, MaxFireLightRadius, multiplieRemainingTime);
            _fireLight.range = lightRange;
        }

        private void UpdateFireScale(float multiplier)
        {
            float fireScale = Mathf.Lerp(MinFireParticlesMultiplier, MaxFireParticlesMultiplier, multiplier);
            _fireParticles.transform.localScale = Vector3.one * fireScale;
        }

        private void SetLevel(int currentLevel)
        {
            int index = currentLevel - 1;
            SetModel(index);
        }

        private void SetModel(int index)
        {
            foreach (GameObject model in _campfireModels) 
                model.SetActive(false);
            
            _campfireModels[index].SetActive(true);
        }

        private int CalculateLevel(float remainingTime)
        {
            if (IsThirdThird(remainingTime))
                return 3;
            if (IsSecondThird(remainingTime))
                return 2;
            else
                return 1;
        }

        private bool IsSecondThird(float remainingTime) => 
            remainingTime > (_campfireLifetime/3);

        private bool IsThirdThird(float remainingTime) => 
            remainingTime > (_campfireLifetime/3) * 2;

        private bool CanFillCampfire() => 
            _player.Carriable is Firewood;
    }
}