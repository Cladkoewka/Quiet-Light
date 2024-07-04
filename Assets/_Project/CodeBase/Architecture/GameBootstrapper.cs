using _Project.CodeBase.Constants;
using _Project.CodeBase.Services.SceneLoad;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Architecture
{
    public class GameBootstrapper : MonoBehaviour 
    {
        private ISceneLoadService _sceneLoadService;

        [Inject]
        public void Init(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        private void Awake()
        {
            StartGame();
        }

        private void StartGame()
        {
            _sceneLoadService.LoadScene(Scenes.MENU);
        }
    }
}