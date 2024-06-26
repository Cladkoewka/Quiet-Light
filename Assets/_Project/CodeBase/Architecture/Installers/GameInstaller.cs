using _Project.CodeBase.Constants;
using _Project.CodeBase.Services.Input;
using _Project.CodeBase.Services.SceneLoad;
using _Project.CodeBase.UI;
using _Project.CodeBase.Utils;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Architecture.Installers
{
    public class GameInstaller : MonoInstaller
    {
        private LoadingCurtain _loadingCurtain;
        private CoroutineRunner _coroutineRunner;
        
        public override void InstallBindings()
        {
            LoadCoroutineRunner();
            LoadCurtain();
            Container.Bind<ISceneLoadService>().FromInstance(new SceneLoadService(_loadingCurtain, _coroutineRunner)).AsSingle();
            Container.Bind<IInputService>().FromInstance(new DekstopInputService()).AsSingle();
        }

        private void LoadCoroutineRunner()
        {
            _coroutineRunner = new GameObject("[CoroutineRunner]").AddComponent<CoroutineRunner>();
            Object.DontDestroyOnLoad(_coroutineRunner);
        }

        private void LoadCurtain()
        {
            var prefabUIRoot = Resources.Load<LoadingCurtain>(Paths.LoadingCurtain);
            _loadingCurtain = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_loadingCurtain.gameObject);
        }
    }
}