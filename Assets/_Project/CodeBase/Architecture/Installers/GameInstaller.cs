using _Project.CodeBase.Constants;
using _Project.CodeBase.Services.Audio;
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
        private AudioManager _audioManager;
        
        public override void InstallBindings()
        {
            LoadCoroutineRunner();
            LoadCurtain();
            LoadAudioManager();
            Container.Bind<ISceneLoadService>().FromInstance(new SceneLoadService(_loadingCurtain, _coroutineRunner)).AsSingle();
            Container.Bind<IInputService>().FromInstance(new DekstopInputService()).AsSingle();
            Container.Bind<AudioManager>().FromInstance(_audioManager).AsSingle();
        }

        private void LoadAudioManager()
        {
            var prefabAudioManager = Resources.Load<AudioManager>(Paths.AudioManager);
            _audioManager = Instantiate(prefabAudioManager);
            DontDestroyOnLoad(_audioManager.gameObject);
        }

        private void LoadCoroutineRunner()
        {
            _coroutineRunner = new GameObject("[CoroutineRunner]").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(_coroutineRunner);
        }

        private void LoadCurtain()
        {
            var prefabUIRoot = Resources.Load<LoadingCurtain>(Paths.LoadingCurtain);
            _loadingCurtain = Instantiate(prefabUIRoot);
            DontDestroyOnLoad(_loadingCurtain.gameObject);
        }
    }
}