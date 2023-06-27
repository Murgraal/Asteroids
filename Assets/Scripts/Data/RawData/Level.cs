using System;
using Systems.Messaging;
using UnityEngine;

namespace Data.RawData
{
    [Serializable]
    public class Level
    {
        public int AsteroidCount => _asteroidCount;
        public int SaucerCount => _saucerCount;

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
                NotificationSystem.Notify(NotificationType.LevelFinished);
            }
        }
        
        [SerializeField] private int _asteroidCount;
        [SerializeField] private int _saucerCount;
    }
}