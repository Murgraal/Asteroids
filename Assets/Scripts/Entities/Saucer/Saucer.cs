using System;
using Data.ScriptableObjects;
using Entities.General;
using Entities.Player;
using Gameflow;
using Systems.Messaging;
using UnityEngine;
using Zenject;

namespace Entities.Saucer
{
    public class Saucer : GameEntity
    {

        [Inject] private GameEntityFactory<Projectile> _projectileFactory;
        [Inject] private GameEntityFactory<Saucer> _saucerFactory;
        [Inject] private GameDataContainer _gameData;
        
        [SerializeField] private float _shootInterval;
        [SerializeField] private Rigidbody2D _rigid;
        [SerializeField] private float _projectileSpeed = 1;
        [SerializeField] private float _flySpeed;
        
        private float _shootTimer;
        
        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            GameplayFunctions.LaunchInRandomDirection(_rigid,_flySpeed);
        }

        public override void OnDespawned()
        {
            NotificationSystem.Notify(NotificationType.SaucerDespawned);
        }

        public override void OnSpawned()
        {
            NotificationSystem.Notify(NotificationType.SaucerSpawned);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("PlayerProjectile"))
            {
                _saucerFactory.Despawn(this);
            }
        }

        private void Update()
        {
            _shootTimer += Time.deltaTime;

            if (_shootTimer >= _shootInterval)
            {
                var shootDir = _gameData.Data.PlayerPosition - transform.position;
                ShipFunctions.Shoot(transform.position,shootDir,_projectileSpeed,_projectileFactory, "EnemyProjectile", "EnemyProjectile");
                _shootTimer = 0;
            }
        }
    }
}
