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
        
        public void Initialize(bool [,] spawnGrid,bool launchRelativeToHitObject = false, Vector3 hitObjectPosition = new Vector3())
        {
            _rigid = GetComponent<Rigidbody2D>();
            _hit = false;
            _spawnGrid = spawnGrid;
            _fragments = AsteroidFunctions.SpawnFragments(_spawnGrid, transform,_fragmentFactory);
            _rigid.mass = _fragments.Count;
            
            if (launchRelativeToHitObject)
            {
                var delta = transform.position - hitObjectPosition;
                _rigid.AddForce(delta.normalized * _flySpeed,ForceMode2D.Impulse);
            }
            else
            {
                GameplayFunctions.LaunchInRandomDirection(_rigid,_flySpeed);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerProjectile") && !_hit)
            {
                _hit = true;
                AsteroidFunctions.HalveAsteroid(_spawnGrid,this,_asteroidFactory,collision.transform);
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