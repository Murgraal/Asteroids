using Gameflow;
using UnityEngine;
using Zenject;

namespace Entities.General
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : GameEntity
    {
        private Rigidbody2D _rigid;
        [SerializeField] private Collider2D _col;
        public Rigidbody2D Rigid => _rigid;

        private float _despawnTimer;
        [SerializeField] private float _timeToDespawn;

        private bool _setToDespawn;

        [Inject] private GameEntityFactory<Projectile> _projectileFactory;
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

        public void MarkForDespawn()
        {
            _setToDespawn = true;
        }

        public override void Reset()
        {
            _despawnTimer = 0;
            _col.enabled = true;
            _setToDespawn = false;
        }

        private void Despawn()
        {
            _projectileFactory.Despawn(this);
        }
    }
}
