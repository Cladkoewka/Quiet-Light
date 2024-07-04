using System;
using _Project.CodeBase.Constants;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.GameLogic.GameplayLogic.Fire;
using _Project.CodeBase.GameLogic.PlayerLogic;
using _Project.CodeBase.Services.Audio;
using _Project.CodeBase.Services.Input;
using _Project.CodeBase.Services.SceneLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI.HUD
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private TMP_Text _gameOverText;
        [SerializeField] private Button _menuButton;
        
        private Campfire _campfire;
        private PlayerCold _playerCold;
        private ISceneLoadService _sceneLoadService;
        private IInputService _inputService;
        private Player _player;
        private AudioManager _audioManager;


        [Inject]
        public void Init(Campfire campfire, PlayerCold playerCold, ISceneLoadService sceneLoadService, IInputService inputService, Player player, AudioManager audioManager)
        {
            _campfire = campfire;
            _playerCold = playerCold;
            _sceneLoadService = sceneLoadService;
            _inputService = inputService;
            _player = player;
            _audioManager = audioManager;
            
            _gameOverScreen.SetActive(false);
            _menuButton.onClick.AddListener(GoToMenu);

            _campfire.OnFaded += CampfireOnFaded;
            _playerCold.OnCold += PlayerColdOnCold;
        }

        private void GoToMenu() => 
            _sceneLoadService.LoadScene(Scenes.MENU);

        private void PlayerColdOnCold()
        {
            SetGameOver();
            _gameOverText.text = "You're cold.";
        }

        private void CampfireOnFaded()
        {
            SetGameOver();
            _gameOverText.text = "Campfire is faded.";
        }

        private void SetGameOver()
        {
            _gameOverScreen.SetActive(true);
            _inputService.SetCursor(true);
            _player.gameObject.SetActive(false);
            DisableSound();
        }

        private void DisableSound()
        {
            _audioManager.SetFootstepSound(false);
            _audioManager.SetCutAxeSound(false);
            _audioManager.SetHearbeatSound(false);
        }
    }
}