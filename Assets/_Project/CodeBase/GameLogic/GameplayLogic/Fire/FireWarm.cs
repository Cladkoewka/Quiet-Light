using System;
using UnityEngine;

namespace _Project.CodeBase.GameLogic.GameplayLogic.Fire
{
    public class FireWarm : MonoBehaviour
    {
        private const float MinRadius = 1;
        private const float MaxRadius = 5;
        
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private float _givingLifetimePerSecond;

        public float GivingLifetimePerSecond => _givingLifetimePerSecond;

        private float _currentRadius;

        private void Awake()
        {
            _currentRadius = MaxRadius;
            _collider.radius = MaxRadius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, _currentRadius);
        }

        public void UpdateRadius(float multiplier)
        {
            _currentRadius = Mathf.Lerp(MinRadius, MaxRadius, multiplier);
            _collider.radius = _currentRadius;
        }

        public void StopWarm()
        {
            _currentRadius = 0;
            _collider.enabled = false;
        }

    }
}