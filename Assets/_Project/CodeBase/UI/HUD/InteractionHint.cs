using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI.HUD
{
    public class InteractionHint : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hintText;
        

        public void SetHintText(string text)
        {
            _hintText.text = text;
        }
    }
}