using Data.ScriptableObjects;
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
            Container.BindInstance(_gameDataContainer).AsSingle().NonLazy();
            _gameDataContainer.Reset();
            var gm = Container.InstantiatePrefab(_gameStateManagerPrefab).GetComponent<GameStateManager>();
            Container.BindInstance(gm).AsSingle().NonLazy();
        }
    }
}
