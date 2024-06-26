using System.Collections;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loadingScreen;
        [SerializeField] private float _fadeTime = 2f;

        private void Awake()
        {
            _loadingScreen.alpha = 0;
        }

        public void HideLoadingScreen() => 
            StartCoroutine(HideLoadingScreenFade());

        public void ShowLoadingScreen()
        {
            _loadingScreen.alpha = 1;
        }

        private IEnumerator HideLoadingScreenFade()
        {
            float elapsedTime = 0;
            float startAlpha = 1;
            float endAlpha = 0;

            while (elapsedTime < _fadeTime)
            {
                _loadingScreen.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / _fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _loadingScreen.alpha = endAlpha;
        }
    }
}