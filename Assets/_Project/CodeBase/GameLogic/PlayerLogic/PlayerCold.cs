using System;
using _Project.CodeBase.GameLogic.GameplayLogic.Fire;
using UnityEngine;

namespace _Project.CodeBase.GameLogic.PlayerLogic
{
    public class PlayerCold : MonoBehaviour
    {
        [SerializeField] private float _lifeTimeInCold;

        public float RemainingLifeMultiplier => _remainingLifeTime / _lifeTimeInCold;
        
        private float _remainingLifeTime;
        private bool _isCold;

        public event Action OnCold;

        private void Awake()
        {
            _remainingLifeTime = _lifeTimeInCold;
        }

        private void Update()
        {
            if (_remainingLifeTime < 0)
            {
                Cold();
                return;
            }
            
            _remainingLifeTime -= Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            FireWarm fireWarm = other.GetComponent<FireWarm>();
            if (fireWarm != null)
                GetWarm(fireWarm);

        }

        private void GetWarm(FireWarm fireWarm)
        {
            _remainingLifeTime += Time.deltaTime * fireWarm.GivingLifetimePerSecond;
            if (_remainingLifeTime > _lifeTimeInCold)
                _remainingLifeTime = _lifeTimeInCold;
        }

        private void Cold()
        {
            if (_isCold)
                return;
            
            OnCold?.Invoke();
            _isCold = true;
        }
    }
}