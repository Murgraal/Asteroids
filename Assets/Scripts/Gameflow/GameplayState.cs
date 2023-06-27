using System.Collections;
using System.Collections.Generic;
using Data.RawData;
using Data.ScriptableObjects;
using Entities;
using Entities.Asteroid;
using Entities.General;
using Entities.Saucer;
using Systems.InputHandling;
using Systems.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject.SpaceFighter;

namespace Gameflow
{
    public class GameplayState : MonoBehaviour
    {
        [Inject] private InputData _inputData;
        [Inject] private GameEntityFactory<Asteroid> _asteroidFactory;
        [Inject] private GameDataContainer _gameDataContainer;
        [Inject] private LevelContainer _levelContainer;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private GameEntityFactory<Saucer> _saucerFactory;

        [SerializeField] private Level _currentLevel;
        
        private void OnEnable()
        {
            NotificationSystem.OnNotify += OnNotificationReceived;
        }

        private void OnDisable()
        {
            NotificationSystem.OnNotify -= OnNotificationReceived;
        }
        
        private void OnNotificationReceived(NotificationType notificationType)
        {
            if (notificationType == NotificationType.PlayerDied)
            {
                PlayerDied();
            }

            if (notificationType == NotificationType.LevelFinished)
            {
                LevelFinished();
            }

            if (notificationType == NotificationType.SaucerSpawned)
            {
                _gameDataContainer.Data.Score += 10;
                _currentLevel.AddSaucerCount();
            }

            if (notificationType == NotificationType.SaucerDespawned)
            {
                _currentLevel.ReduceSaucerCount();
            }

            if (notificationType == NotificationType.AsteroidDespawned)
            {
                _gameDataContainer.Data.Score += 5;
                _currentLevel.ReduceAsteroidCount();
            }

            if (notificationType == NotificationType.AsteroidSpawned)
            {
                _currentLevel.AddAsteroidCount();
            }
        }
        
        public void Start()
        {
            _gameDataContainer.Data.PlayFieldBoundaries = new PlayField(
                Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y,
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x);
            
            StartLevel(_gameDataContainer.Data.Level);
        }
        
        private void StartLevel(int currentLevel)
        {
            if (currentLevel >= _levelContainer.Levels.Count)
            {
                _gameStateManager.GoToScoreScreen();
            }
            
            var levelConf = _levelContainer.Levels[currentLevel];

            _currentLevel = new Level();
            
            var asteroidCount = levelConf.AsteroidCount;
            var saucerCount = levelConf.SaucerCount;
            
            var asteroids = SpawnEntities(asteroidCount,_asteroidFactory);

            foreach (var asteroid in asteroids)
            {
                asteroid.Initialize(AsteroidFunctions.GetFragmentSpawnGrid(4,100));
                asteroid.transform.position = GameplayFunctions.GetRandomPositionOnScreen();
            }
            
            var saucers = SpawnEntities(saucerCount,_saucerFactory);

            foreach (var saucer in saucers)
            {
                saucer.transform.position = GameplayFunctions.GetRandomPositionOnScreen();
            }
        }

        private void LevelFinished()
        {
            _gameDataContainer.Data.Level++;
            StartLevel(_gameDataContainer.Data.Level);
        }

        private void PlayerDied()
        {
            _gameDataContainer.Data.Lives--;
            
            SceneManager.LoadScene("Gameplay");
            
            if (_gameDataContainer.Data.Lives <= 0)
            {
                _gameStateManager.GoToScoreScreen();
            }
            else
            {
                StartLevel(_gameDataContainer.Data.Level);
            }
        }

        private List<T> SpawnEntities<T>(int count, GameEntityFactory<T> factory) where T : GameEntity
        {
            var result = new List<T>();
            
            for (int i = 0; i < count; i++)
            {
                var spawn = factory.Spawn();
                result.Add(spawn);
            }

            return result;
        }
        
        private void Update()
        {
            InputFunctions.UpdatePlayerInputKeyboard(ref _inputData);;
        }
    }
}