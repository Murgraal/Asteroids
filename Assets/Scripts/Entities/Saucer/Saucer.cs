using System;
using Data.ScriptableObjects;
using Entities.General;
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
        [SerializeField] private float _shootInterval;
        private float _shootTimer;
        
        private void Update()
        {
            _shootTimer += Time.deltaTime;

            if (_shootTimer >= _shootInterval)
            {
                ShipFunctions.Shoot(transform.position,_gameData.Data.PlayerPosition,_projectileFactory);
                _shootTimer = _shootInterval;
            }
        }
    }
}
