﻿using System;
using Data.ScriptableObjects;
using Entities.General;
using Gameflow;
using Systems.Messaging;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class PlayerShip : GameEntity
    {
        [Inject] private PlayerSettings _settings;
        [Inject] private InputData _inputData;
        [Inject] private GameEntityFactory<Projectile> _projectileFactory;
        [Inject] private GameDataContainer _gameData;
        private Rigidbody2D _rigid;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            Instantiate(_settings.ShipModel, transform.position, _settings.ShipModel.transform.rotation, transform);
            _rigid.freezeRotation = true;
        }

        private void Update()
        {
            _gameData.Data.PlayerPosition = transform.position;
            
            if (_inputData.FirePressed)
            {
                ShipFunctions.Shoot(transform.position,transform.right, _settings.ProjectileSpeed,_projectileFactory, "PlayerProjectile", "PlayerProjectile");
            }
            
            ShipFunctions.RotateShip(_inputData, transform, _settings.RotateSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("EnemyProjectile") || collision.transform.CompareTag("Asteroid"))
            {
                NotificationSystem.Notify(NotificationType.PlayerDied);
            }
        }

        private void FixedUpdate()
        {
            if (_inputData.ForwardPressed)
            {
                _rigid.AddForce(transform.right * _settings.Speed);
            }
        }
        
    }
}