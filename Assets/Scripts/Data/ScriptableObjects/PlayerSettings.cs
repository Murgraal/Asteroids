using Entities.Player;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public float Speed;
        public float RotateSpeed;
        public float ProjectileSpeed;
        public GameObject ShipModel;
        public Projectile ProjectilePrefab;
    }
}