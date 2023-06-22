using System;
using Entities.Player;
using Gameflow;
using UnityEngine;
using Zenject;

namespace Entities.Saucer
{
    public class Saucer : GameEntity
    {

        [Inject] private GameEntityFactory<Projectile> _projectileFactory;
        [Inject] private GameDataContainer _gameData;
        private float _shootInterval;
        private float _shootTimer;


        private void Update()
        {
            _shootTimer += Time.deltaTime;

            if (_shootTimer >= _shootInterval)
            {
                ShipFunctions.Shoot(transform.position,_gameData.Data.PlayerPosition,_projectileFactory);
            }
        }
    }

    public class GameDataContainer : ScriptableObject
    {
        public GameData Data;

        public void Reset()
        {
            Data = new GameData();
        }
    }

    public class GameData
    {
        public Vector2 PlayerPosition;
        public int Score = 0;
        public int Lives = 5;
        public int Level = 1;
    }
}
