using System;
using Gameflow;
using UnityEngine;
using Zenject;

namespace Entities.General
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : GameEntity
    {
        public Rigidbody2D Rigid => _rigid;
        
        [SerializeField] private Collider2D _col;
        [SerializeField] private float _timeToDespawn;
        [SerializeField] private Rigidbody2D _rigid;

        [Inject] private GameEntityFactory<Projectile> _projectileFactory;
        
        private float _despawnTimer;
        private bool _setToDespawn;
        
        
        void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _despawnTimer += Time.deltaTime;
            if (_despawnTimer >= _timeToDespawn)
            {
                _setToDespawn = true;
            }
            if (_setToDespawn)
            {
                Despawn();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            MarkForDespawn();
        }

        public void MarkForDespawn()
        {
            _col.enabled = false;
            _rigid.simulated = false;
            _setToDespawn = true;
        }

        public override void Reset()
        {
            _despawnTimer = 0;
            _col.enabled = true;
            _rigid.simulated = true;
            _setToDespawn = false;
        }

        private void Despawn()
        {
            _projectileFactory.Despawn(this);
        }
    }
}
