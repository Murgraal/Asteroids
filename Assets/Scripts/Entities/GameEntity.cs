using UnityEngine;

namespace Entities
{
    public abstract class GameEntity :  MonoBehaviour
    {
        public virtual void OnSpawned(){}
        public virtual void OnDespawned(){}
        public virtual void Reset(){}
    }
}