using UnityEngine;

namespace Entities.Asteroid
{
    public class AsteroidFragment : GameEntity
    {
        [SerializeField] private Rigidbody2D _rigid;
        public Rigidbody2D Rigid => _rigid;
    }
}