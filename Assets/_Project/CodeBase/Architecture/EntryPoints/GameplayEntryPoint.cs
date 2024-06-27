using System;
using _Project.CodeBase.Constants;
using _Project.CodeBase.GameLogic.Camera;
using _Project.CodeBase.GameLogic.PlayerLogic;
using _Project.CodeBase.UI.HUD;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Architecture.EntryPoints
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Inject]
        private DiContainer _diContainer;

        private Player _player;

        private void Awake()
        {
            InitWorld();
            InitPlayer();
            InitCamera();
            InitUI();
        }

        private void InitPlayer()
        {
            var playerPrefab = Resources.Load(Paths.Player);
            GameObject playerGameObject = _diContainer.InstantiatePrefab(playerPrefab);
            _player = playerGameObject.GetComponent<Player>();

        }

        private void InitWorld()
        {
            
        }

        private void InitCamera()
        {
            var cameraPrefab = Resources.Load(Paths.CameraRoot);
            GameObject cameraGameObject = _diContainer.InstantiatePrefab(cameraPrefab);
            CameraRoot cameraRoot = cameraGameObject.GetComponent<CameraRoot>();
            cameraRoot.Construct(_player.transform);
        }

        private void InitUI()
        {
            var gameHUDPrefab = Resources.Load(Paths.GameHUD);
            GameObject gameHUDGameObject = _diContainer.InstantiatePrefab(gameHUDPrefab);
            GameHud gameHud = gameHUDGameObject.GetComponent<GameHud>();
            InteractionTrigger interactionTrigger = _player.GetComponent<InteractionTrigger>();
            gameHud.Construct(interactionTrigger);
        }
    }
}
