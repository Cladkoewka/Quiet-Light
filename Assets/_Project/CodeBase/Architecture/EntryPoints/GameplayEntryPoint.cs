using System;
using _Project.CodeBase.Constants;
using _Project.CodeBase.GameLogic.Camera;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Architecture.EntryPoints
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Inject]
        private DiContainer _diContainer;

        private GameObject _playerGameObject;

        private void Awake()
        {
            InitWorld();
            InitPlayer();
            InitCamera();
        }

        private void InitPlayer()
        {
            var playerPrefab = Resources.Load(Paths.Player);
            _playerGameObject = _diContainer.InstantiatePrefab(playerPrefab);

        }

        private void InitWorld()
        {
            
        }

        private void InitCamera()
        {
            var cameraPrefab = Resources.Load(Paths.CameraRoot);
            GameObject cameraGameObject = _diContainer.InstantiatePrefab(cameraPrefab);
            cameraGameObject.GetComponent<CameraRoot>().Init(_playerGameObject.transform);
        }
    }
}
