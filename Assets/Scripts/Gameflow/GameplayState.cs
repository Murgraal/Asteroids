using System;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Entities.Asteroid;
using Entities.Saucer;
using Systems.InputHandling;
using UnityEngine;
using Zenject;

namespace Gameflow
{
    public class GameplayState : MonoBehaviour
    {
        public const string SceneName = "Gameplay";
    
        [Inject] private InputData _inputData;
        [Inject] private GameEntityFactory<Asteroid> _asteroidFactory;
        [Inject] private GameDataContainer _gameDataContainer;
        [Inject] private LevelContainer _levelContainer;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private GameEntityFactory<Saucer> _saucerFactory;

        private Level _currentLevel;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
           
        }

        public void Start()
        {
            _gameDataContainer.Reset();
            var asteroid = _asteroidFactory.Spawn();
            asteroid.transform.position = Vector3.zero;
            asteroid.InitAsteroid(AsteroidFunctions.GetFragmentSpawnGrid(4,70));
        }

        private void StartLevel(int currentLevel)
        {
            if (currentLevel >= _levelContainer.Levels.Count)
            {
                _gameStateManager.GoToScoreScreen();
            }
            var level = _levelContainer.Levels[currentLevel];

            _currentLevel = new Level(level);
            SpawnAmount(_currentLevel.AsteroidCount,_asteroidFactory);
            SpawnAmount(_currentLevel.SaucerCount,_saucerFactory);

            _currentLevel.LevelFinished += LevelFinished;
        }

        private void LevelFinished()
        {
            _currentLevel.LevelFinished -= LevelFinished;
            _gameDataContainer.Data.Level++;
            StartLevel(_gameDataContainer.Data.Level);
        }

        private void SpawnAmount<T>(int count, GameEntityFactory<T> factory) where T : GameEntity
        {
            for (int i = 0; i < count; i++)
            {
                factory.Spawn();
            }
        }
        
        private void Update()
        {
            InputFunctions.UpdatePlayerInputKeyboard(ref _inputData);;
        }
    }

    public class LevelContainer : ScriptableObject
    {
        public List<Level> Levels;
    }

    public class Level
    {
        public event Action LevelFinished;
        public int AsteroidCount => _asteroidCount;
        public int SaucerCount => _saucerCount;
        public Level(Level initData)
        {
            _asteroidCount = initData._asteroidCount;
            _saucerCount = initData._saucerCount;
        }

        public void AddAsteroidCount()
        {
            _asteroidCount++;
        }
        
        public void AddSaucerCount()
        {
            _saucerCount++;
        }
        
        public void ReduceAsteroidCount()
        {
            _asteroidCount--;
            InformIfLevelFinished();
        }
        
        public void ReduceSaucerCount()
        {
            _saucerCount--;
            InformIfLevelFinished();
        }

        public void InformIfLevelFinished()
        {
            if (_saucerCount == 0 && _asteroidCount == 0)
            {
                LevelFinished?.Invoke();
            }
        }
        
        private int _asteroidCount;
        private int _saucerCount;
    }
    
    
}