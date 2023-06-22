using Gameflow;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : GameEntity
    {
        private Rigidbody2D _rigid;
        public Rigidbody2D Rigid => _rigid;

        private float _despawnTimer;
        private bool hasHit;
    
        [Inject] private GameEntityFactory<Projectile> _projectileFactory;
        void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _despawnTimer += Time.deltaTime;
            if (_despawnTimer >= 5f && !hasHit)
            {
                Despawn();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (hasHit) return;
            hasHit = true;
            Despawn();
        }

        public override void Reset()
        {
            hasHit = false;
            _despawnTimer = 0;
        }

        private void Despawn()
        {
            _projectileFactory.Despawn(this);
        }
    }
}
