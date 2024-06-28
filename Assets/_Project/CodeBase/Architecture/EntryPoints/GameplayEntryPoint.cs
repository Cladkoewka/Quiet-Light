using System;
using _Project.CodeBase.Constants;
using _Project.CodeBase.GameLogic.Camera;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.GameLogic.PlayerLogic;
using _Project.CodeBase.UI.HUD;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Architecture.EntryPoints
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _treePrefab;
        [SerializeField] private GameObject _benchPrefab;
        [SerializeField] private Transform _treeSpawnPoint;
        [SerializeField] private Transform _benchSpawnPoint;
        
        [Inject]
        private DiContainer _diContainer;


        private void Awake()
        {
            InitPlayer();
            InitWorld();
            InitCamera();
            InitUI();
        }

        private void InitPlayer()
        {
            var playerPrefab = Resources.Load(Paths.Player);
            GameObject playerGameObject = _diContainer.InstantiatePrefab(playerPrefab);
            Player player = playerGameObject.GetComponent<Player>();
            _diContainer.BindInstance(player).AsSingle();
            InteractionTrigger interactionTrigger = playerGameObject.GetComponent<InteractionTrigger>();
            _diContainer.BindInstance(interactionTrigger).AsSingle();
            //PlayerController playerController = playerGameObject.GetComponent<PlayerController>();
            //_diContainer.BindInstance(playerController).AsSingle();
        }

        private void InitWorld()
        {
            _diContainer.InstantiatePrefab(_treePrefab, _treeSpawnPoint);
            GameObject benchGameObject = _diContainer.InstantiatePrefab(_benchPrefab, _benchSpawnPoint);
            Bench bench = benchGameObject.GetComponent<Bench>();
            _diContainer.BindInstance(bench).AsSingle();
        }

        private void InitCamera()
        {
            var cameraPrefab = Resources.Load(Paths.CameraRoot);
            GameObject cameraGameObject = _diContainer.InstantiatePrefab(cameraPrefab);
        }

        private void InitUI()
        {
            var gameHUDPrefab = Resources.Load(Paths.GameHUD);
            GameObject gameHUDGameObject = _diContainer.InstantiatePrefab(gameHUDPrefab);
            GameHud gameHud = gameHUDGameObject.GetComponent<GameHud>();
        }
    }
}
