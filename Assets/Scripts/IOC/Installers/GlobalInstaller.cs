using Entities.Saucer;
using Gameflow;
using UnityEngine;
using Zenject;

namespace IOC.Installers
{
    public class GlobalInstaller : MonoInstaller<GlobalInstaller>
    {
        [SerializeField] private GameStateManager _gameStateManagerPrefab;

        [SerializeField] private GameDataContainer _gameDataContainer;
        
        public override void InstallBindings()
        {
            var gm = Container.InstantiatePrefab(_gameStateManagerPrefab);
            Container.BindInstance(gm).AsSingle().NonLazy();
            Container.BindInstance(_gameDataContainer).AsSingle();
        }
    }
}
