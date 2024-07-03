using System.Collections;
using System.Collections.Generic;
using _Project.CodeBase.Constants;
using _Project.CodeBase.UI;
using _Project.CodeBase.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.CodeBase.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private LoadingCurtain _uiRoot;
        private CoroutineRunner _coroutineRunner;

        public SceneLoadService(LoadingCurtain uiRoot, CoroutineRunner coroutineRunner)
        {
            _uiRoot = uiRoot;
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string sceneName)
        {
            //SceneManager.LoadScene(sceneName);
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            _uiRoot.ShowLoadingScreen();

            yield return SceneManager.LoadSceneAsync(Scenes.EMPTY);
            yield return SceneManager.LoadSceneAsync(sceneName);
            
            _uiRoot.HideLoadingScreen();
        }
    }
}