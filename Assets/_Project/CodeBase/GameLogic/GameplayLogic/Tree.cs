using UnityEngine;

namespace _Project.CodeBase.GameLogic.GameplayLogic
{
    public class Tree : MonoBehaviour
    {
        public void Cut()
        {
            Destroy(gameObject);
        }
    }
}