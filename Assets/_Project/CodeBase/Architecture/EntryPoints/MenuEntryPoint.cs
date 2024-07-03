using _Project.CodeBase.Constants;
using _Project.CodeBase.Services.Audio;
using _Project.CodeBase.Services.Input;
using _Project.CodeBase.UI;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Architecture.EntryPoints
{
    public class MenuEntryPoint : MonoBehaviour
    {
        
        private DiContainer _diContainer;
        private IInputService _inputService;
        private AudioManager _audioManager;


        [Inject]
        public void Init(DiContainer diContainer, IInputService inputService, AudioManager audioManager)
        {
            _inputService = inputService;
            _diContainer = diContainer;
            _audioManager = audioManager;
        }
        private void Awake()
        {
            InitMenu();
            InitMenuWorld();
        }

        private void InitMenuWorld()
        {
            var menuWorld = Resources.Load(Paths.MenuWorld);
            Object.Instantiate(menuWorld);
        }

        private void InitMenu()
        {
            var menuPrefab = Resources.Load<MainMenu>(Paths.MainMenu);
            _diContainer.InstantiatePrefab(menuPrefab);
            _inputService.SetCursor(true);
            _audioManager.SetMusic(true);
        }
    }
}