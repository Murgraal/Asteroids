using System;
using System.Collections.Generic;
using Entities.General;
using Gameflow;
using Systems.Messaging;
using UnityEngine;
using Zenject;

namespace Entities.Asteroid
{
    public class Asteroid : GameEntity
    {
        [Inject] private GameEntityFactory<AsteroidFragment> _fragmentFactory;
        [Inject] private GameEntityFactory<Asteroid> _asteroidFactory;
        [SerializeField] private List<AsteroidFragment> _fragments = new List<AsteroidFragment>();
        [SerializeField] private float _flySpeed;

        private bool _hit;
        private bool[,] _spawnGrid;

        private Rigidbody2D _rigid;

        public bool[,] SpawnGrid => _spawnGrid;

        

        public void Initialize(bool [,] spawnGrid)
        {
            _rigid = GetComponent<Rigidbody2D>();
            _hit = false;
            _spawnGrid = spawnGrid;
            _fragments = AsteroidFunctions.SpawnFragments(_spawnGrid, transform,_fragmentFactory);
            var delta = (Vector3)GameplayFunctions.GetRandomPositionOnScreen() - transform.position;
            _rigid.AddForce(delta * _flySpeed,ForceMode2D.Impulse);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Projectile") && !_hit)
            {
                collision.transform.GetComponent<Projectile>().MarkForDespawn();
                AsteroidFunctions.HalveAsteroid(_spawnGrid,this,_asteroidFactory);
            }
        }
        
        public void Despawn()
        {
            foreach (var fragment in _fragments)
            {
                _fragmentFactory.Despawn(fragment);
            }
            
            _asteroidFactory.Despawn(this);
        }

        public override void OnSpawned()
        {
             NotificationSystem.Notify(NotificationType.AsteroidSpawned);
        }

        public override void OnDespawned()
        {
            NotificationSystem.Notify(NotificationType.AsteroidDespawned);
        }
    }
}