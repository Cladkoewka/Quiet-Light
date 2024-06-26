using UnityEngine;

namespace _Project.CodeBase.GameLogic
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform _followTransform;

        public void SetTarget(Transform target)
        {
            _followTransform = target;
        }
        
        private void Update()
        {
            transform.position = _followTransform.position;
            transform.rotation = _followTransform.rotation;
        }
    }
}