using _Project.CodeBase.Constants;
using _Project.CodeBase.UI;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Architecture.EntryPoints
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [Inject]
        private DiContainer _diContainer;
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
        }
    }
}