using Entities.Asteroid;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/AsteroidSettings")]
    public class AsteroidSettings : ScriptableObject
    {
        public float Speed;
        public int Size;
        public AsteroidFragment FragmentPrefab;
    }
}