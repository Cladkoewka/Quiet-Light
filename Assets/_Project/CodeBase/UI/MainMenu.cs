using _Project.CodeBase.Constants;
using _Project.CodeBase.Services.Audio;
using _Project.CodeBase.Services.SceneLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class MainMenu : MonoBehaviour
    {
        private const string IsMusicOnKey = "IsMusicOn";
        [SerializeField] private Button _playButton;
        [SerializeField] private Toggle _musicToggle;
        
        private ISceneLoadService _sceneLoadService;
        private AudioManager _audioManager;

        [Inject]
        public void Init(ISceneLoadService sceneLoadService, AudioManager audioManager)
        {
            _sceneLoadService = sceneLoadService;
            _audioManager = audioManager;
        }
        
        private void Awake()
        {
            _playButton.onClick.AddListener(StartGame);
            _musicToggle.onValueChanged.AddListener(SetMusic);
            SetMusic();
        }

        private void SetMusic()
        {
            bool isMusicOn = PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
            _musicToggle.isOn = isMusicOn;
            _audioManager.SetMusic(isMusicOn);
        }

        private void SetMusic(bool isMusicOn)
        {
            _audioManager.SetMusic(isMusicOn);
            PlayerPrefs.SetInt(IsMusicOnKey, isMusicOn ? 1 : 0);
        }

        private void StartGame()
        {
            Debug.Log("PlayButton");
            _sceneLoadService.LoadScene(Scenes.GAMEPLAY);
        }
    }
}