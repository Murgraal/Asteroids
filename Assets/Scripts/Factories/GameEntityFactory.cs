using System;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Gameflow
{
    public class GameEntityFactory<T> : MemoryPool<T> where T : GameEntity 
    {
        protected override void OnSpawned(T item)
        {
            
        }

        protected override void Reinitialize(T item)
        {
            item.Reset();
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(T item)
        {
            item.gameObject.SetActive(false);
        }
    }
}

public abstract class GameEntity :  MonoBehaviour   
{
    
    public void OnSpawned()
    {
        
    }

    public void OnDespawned()
    {
        
    }
    public virtual void Reset(){}
}