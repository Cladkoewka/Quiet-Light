using _Project.CodeBase.Constants;
using _Project.CodeBase.Services.SceneLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        
        private ISceneLoadService _sceneLoadService;

        [Inject]
        public void Init(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }
        
        private void Awake()
        {
            _playButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            Debug.Log("PlayButton");
            _sceneLoadService.LoadScene(Scenes.GAMEPLAY);
        }
    }
}