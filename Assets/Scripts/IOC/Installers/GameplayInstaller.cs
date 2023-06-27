using Data.ScriptableObjects;
using Entities.Asteroid;
using Entities.General;
using Entities.Player;
using Entities.Saucer;
using Gameflow;
using UnityEngine;
using Zenject;

namespace IOC.Installers
{
    public class GameplayInstaller : MonoInstaller<GameplayInstaller>
    {
        [SerializeField] private PlayerSettings _playerSettings;
        
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private AsteroidFragment _fragmentPrefab;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Saucer _saucerPrefab;
        
        [SerializeField] private InputData _inputData;
        [SerializeField] private LevelContainer _levels;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings).AsSingle().NonLazy();
            Container.BindInstance(_asteroidPrefab).AsSingle().NonLazy();
            Container.BindInstance(_fragmentPrefab).AsSingle().NonLazy();
            Container.BindInstance(_inputData).AsSingle().NonLazy();
            Container.BindInstance(_levels).AsSingle().NonLazy();


            Container.BindMemoryPool<AsteroidFragment, GameEntityFactory<AsteroidFragment>>()
                .FromComponentInNewPrefab(_fragmentPrefab).NonLazy();
            Container.BindMemoryPool<Asteroid, GameEntityFactory<Asteroid>>().FromComponentInNewPrefab(_asteroidPrefab).NonLazy();
            Container.BindMemoryPool<Projectile, GameEntityFactory<Projectile>>().FromComponentInNewPrefab(_projectilePrefab).NonLazy();
            Container.BindMemoryPool<Saucer, GameEntityFactory<Saucer>>().FromComponentInNewPrefab(_saucerPrefab)
                .NonLazy();
        }
    }
}
