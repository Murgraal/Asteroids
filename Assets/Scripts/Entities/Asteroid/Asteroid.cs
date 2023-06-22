using System;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Gameflow;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.Asteroid
{
    public class Asteroid : GameEntity
    {
       
        
        [Inject] private AsteroidSettings _settings;
        [Inject] private GameEntityFactory<AsteroidFragment> _fragmentFactory;
        [Inject] private GameEntityFactory<Asteroid> _asteroidFactory;
        [SerializeField] private List<AsteroidFragment> _fragments = new List<AsteroidFragment>();
        
        private bool[,] _spawnGrid;
        
        public void InitAsteroid(bool [,] spawnGrid)
        {
            _spawnGrid = spawnGrid;
            _fragments = AsteroidFunctions.SpawnFragments(_spawnGrid, transform,_fragmentFactory);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                HalveAsteroid(_spawnGrid);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Projectile"))
            {
                HalveAsteroid(_spawnGrid);
            }
                
        }

        private bool _canHalveX;
        private bool _canHalveY;

        
        
        private void HalveAsteroid(bool [,] existingSpawnGrid)
        {
            var halvedSpawnGrid = DeclareHalvedDimensions(existingSpawnGrid);

            if (halvedSpawnGrid == null)
            {
                Despawn();
                return;
            }
            
            var asteroidSplits = new []{_asteroidFactory.Spawn(),_asteroidFactory.Spawn()};
            
            for(int i = 0; i < asteroidSplits.Length; i++)
            {
                var asteroid = asteroidSplits[i];
                asteroid.transform.position = transform.position;
                asteroid.InitAsteroid(halvedSpawnGrid[i]);
            }

            Despawn();
        }

        private List<bool[,]> DeclareHalvedDimensions(bool [,] existingSpawnGrid)
        {
            var halvedSpawnGrid = new List<bool[,]>();

            var sizeX = _spawnGrid.GetLength(0);
            var sizeY = _spawnGrid.GetLength(1);

            _canHalveX = sizeX > 1;
            _canHalveY = sizeY > 1;
            
            if (_canHalveX && _canHalveY)
            {
                var coinFlip = Random.Range(0, 2);

                if (coinFlip == 0)
                {
                    halvedSpawnGrid = existingSpawnGrid.HalveX();
                }
                else
                {
                    halvedSpawnGrid = existingSpawnGrid.HalveY();
                }
            }
            else if(_canHalveX || _canHalveY)
            {
                if (_canHalveX)
                {
                    halvedSpawnGrid = existingSpawnGrid.HalveX();
                }
                else if (_canHalveY)
                {
                    halvedSpawnGrid = existingSpawnGrid.HalveY();
                }
            }
            else
            {
                return null;
            }

            return halvedSpawnGrid;
        }
        
        
        private void Despawn()
        {
            foreach (var fragment in _fragments)
            {
                _fragmentFactory.Despawn(fragment);
            }
            
            _asteroidFactory.Despawn(this);
        }
    }
}

public static class ArrayExt
{
    public static List<T[,]> HalveX<T>(this T[,] original)
    {
        var result = new List<T[,]>();
        
        var xSize = original.GetLength(0);
        var ySize = original.GetLength(1);
        
        var halfSizeX = xSize / 2;

        for (int i = 0; i < 2; i++)
        {
            result.Add(new T[halfSizeX,ySize]);
        }
        
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (x < halfSizeX)
                {
                    result[0][x, y] = original[x, y];
                }
                else
                {
                    result[1][x - halfSizeX, y] = original[x, y];
                }
            }
        }
        
        return result;
    }
    
    public static List<T[,]> HalveY<T>(this T[,] original)
    {
        var result = new List<T[,]>();
        
        var xSize = original.GetLength(0);
        var ySize = original.GetLength(1);
        
        var halfSizeY = ySize / 2;

        for (int i = 0; i < 2; i++)
        {
            result.Add(new T[xSize,halfSizeY]);
        }
        
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (y < halfSizeY)
                {
                    result[0][x, y] = original[x, y];
                }
                else
                {
                    result[1][x, y - halfSizeY] = original[x, y];
                }
            }
        }
        
        return result;
    }
}
