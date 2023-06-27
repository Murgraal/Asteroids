using System;
using Entities;
using Entities.Player;
using Zenject;

namespace Gameflow
{
    public class GameEntityFactory<T> : MemoryPool<T> where T : GameEntity 
    {
        protected override void OnSpawned(T item)
        {
            item.OnSpawned();
        }

        protected override void Reinitialize(T item)
        {
            item.Reset();
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(T item)
        {
            item.gameObject.SetActive(false);
            item.OnDespawned();
        }
    }
}