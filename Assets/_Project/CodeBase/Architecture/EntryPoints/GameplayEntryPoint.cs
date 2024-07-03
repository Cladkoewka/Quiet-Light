using System;
using _Project.CodeBase.Constants;
using _Project.CodeBase.GameLogic.Camera;
using _Project.CodeBase.GameLogic.GameplayLogic;
using _Project.CodeBase.GameLogic.GameplayLogic.Fire;
using _Project.CodeBase.GameLogic.GameplayLogic.Interactables;
using _Project.CodeBase.GameLogic.PlayerLogic;
using _Project.CodeBase.Services.Audio;
using _Project.CodeBase.Services.Input;
using _Project.CodeBase.UI.HUD;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Architecture.EntryPoints
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private const float TreeSpawnIndentFromZeroCoordinates = 4f;
        private const int GroundSize = 100;

        [Header("Prefabs")]
        [SerializeField] private GameObject _treePrefab;
        [SerializeField] private GameObject _benchPrefab;
        [SerializeField] private GameObject _campfirePrefab;
        
        [Header("World")] 
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _benchSpawnPoint;
        [SerializeField] private Transform _campfireSpawnPoint;
        [SerializeField] private Transform _playerSpawnPoint;

        [Header("Parameters")] [SerializeField]
        private int _treesCount = 20;
        
        
        
        private DiContainer _diContainer;
        private IInputService _inputService;
        private AudioManager _audioManager;

        [Inject]
        public void Init(DiContainer diContainer, IInputService inputService, AudioManager audioManager)
        {
            _diContainer = diContainer;
            _inputService = inputService;
            _audioManager = audioManager;
        }


        private void Awake()
        {
            InitPlayer();
            InitCamera();
            InitWorld();
            InitUI();
            InitSounds();
        }

        private void InitSounds() => 
            _audioManager.SetWindSound(true);

        private void InitPlayer()
        {
            var playerPrefab = Resources.Load(Paths.Player);
            GameObject playerGameObject = _diContainer.InstantiatePrefab(playerPrefab, _playerSpawnPoint.position,
                Quaternion.identity, null);
            Player player = playerGameObject.GetComponent<Player>();
            _diContainer.BindInstance(player).AsSingle();
            InteractionTrigger interactionTrigger = playerGameObject.GetComponent<InteractionTrigger>();
            _diContainer.BindInstance(interactionTrigger).AsSingle();
            PlayerCold playerCold = playerGameObject.GetComponent<PlayerCold>();
            _diContainer.BindInstance(playerCold).AsSingle();
        }

        private void InitWorld()
        {
            InitTrees();
            InitBench();
            InitCampfire();
        }

        private void InitTrees()
        {
            for (int i = 0; i < _treesCount; i++)
            {
                float x = RandomCoordinate();
                float y = RandomCoordinate();
                var spawnPosition = new Vector3(x, 0, y);
                var spawnRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
                _diContainer.InstantiatePrefab(_treePrefab, spawnPosition, spawnRotation, _worldTransform);
            }
            
        }

        

        private void InitCampfire()
        {
            GameObject benchGameObject = _diContainer.InstantiatePrefab(_campfirePrefab, _campfireSpawnPoint.position, 
                Quaternion.identity, _worldTransform);
            Campfire campfire = benchGameObject.GetComponent<Campfire>();
            _diContainer.BindInstance(campfire).AsSingle();
        }

        private void InitBench()
        {
            GameObject benchGameObject = _diContainer.InstantiatePrefab(_benchPrefab, _benchSpawnPoint.position, 
                Quaternion.identity, _worldTransform);
            Bench bench = benchGameObject.GetComponent<Bench>();
            _diContainer.BindInstance(bench).AsSingle();
        }

        private void InitCamera()
        {
            var cameraPrefab = Resources.Load(Paths.CameraRoot);
            GameObject cameraGameObject = _diContainer.InstantiatePrefab(cameraPrefab);
            CameraRoot cameraRoot = cameraGameObject.GetComponent<CameraRoot>();
            _diContainer.BindInstance(cameraRoot).AsSingle();
        }

        private void InitUI()
        {
            var gameHUDPrefab = Resources.Load(Paths.GameHUD);
            GameObject gameHUDGameObject = _diContainer.InstantiatePrefab(gameHUDPrefab);
            GameHud gameHud = gameHUDGameObject.GetComponent<GameHud>();
            _inputService.SetCursor(false);
        }
        
        private static float RandomCoordinate()
        {
            float x;
            while (true)
            {
                x = Random.Range(-GroundSize/2, GroundSize/2);
                if (Math.Abs(x) > TreeSpawnIndentFromZeroCoordinates)
                    break;
            }
            return x;
        }
    }
}
