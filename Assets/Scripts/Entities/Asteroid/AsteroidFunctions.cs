using System.Collections.Generic;
using Gameflow;
using UnityEngine;
using Utils.Extensions;

namespace Entities.Asteroid
{
    public static class AsteroidFunctions
    {
        public static bool[,] GetFragmentSpawnGrid(int size,int stiffness)
        {
            var result = new bool[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    result[x, y] = Random.Range(0, 100) <= stiffness;
                }
            }

            return result;
        }
        
        public static List<bool[,]> DeclareHalvedDimensions(bool [,] existingSpawnGrid)
        {
            var halvedSpawnGrid = new List<bool[,]>();

            var sizeX = existingSpawnGrid.GetLength(0);
            var sizeY = existingSpawnGrid.GetLength(1);

            var canHalveX = sizeX > 2;
            var canHalveY = sizeY > 2;
            
            if (canHalveX && canHalveY)
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
            else if(canHalveX)
            {
                halvedSpawnGrid = existingSpawnGrid.HalveX();
            }
            else if (canHalveY)
            {
                halvedSpawnGrid = existingSpawnGrid.HalveY();
            }
            else
            {
                return null;
            }

            return halvedSpawnGrid;
        }
        
        public static void HalveAsteroid(bool [,] existingSpawnGrid, Asteroid asteroid, GameEntityFactory<Asteroid> asteroidFactory)
        {
            var halvedSpawnGrid = DeclareHalvedDimensions(existingSpawnGrid);

            if (halvedSpawnGrid == null)
            {
                asteroid.Despawn();
                return;
            }
            
            var asteroidSplits = new []{asteroidFactory.Spawn(),asteroidFactory.Spawn()};
            
            for(int i = 0; i < asteroidSplits.Length; i++)
            {
                var newPos = asteroid.transform.position;

                var width = asteroid.SpawnGrid.GetLength(0);
                var height = asteroid.SpawnGrid.GetLength(1);
                
                if (i % 2 == 0)
                {
                    newPos.x +=  width / 2f;
                    newPos.y += height / 2f;
                }
                else
                {
                    newPos.x -= width / 2f;
                    newPos.y -= height / 2f;
                }
                
                var newAsteroid = asteroidSplits[i];
                newAsteroid.transform.position = newPos;
                newAsteroid.Initialize(halvedSpawnGrid[i]);
            }

            asteroid.Despawn();
        }

        public static List<AsteroidFragment> SpawnFragments(bool[,] spawnGrid, Transform parent,GameEntityFactory<AsteroidFragment> fragmentFactory)
        {
            var result = new List<AsteroidFragment>();
            
            for (int x = 0; x < spawnGrid.GetLength(0); x++)
            {
                for (int y = 0; y < spawnGrid.GetLength(1); y++)
                {
                    var shouldSpawn = spawnGrid[x, y];

                    if (shouldSpawn)
                    {
                        var fragment = fragmentFactory.Spawn();
                        fragment.transform.parent = parent; 
                        fragment.transform.localPosition = new Vector3(x, y, 0);
                        result.Add(fragment);
                    }
                }
            }

            if (result.Count == 0)
            {
                var fragment = fragmentFactory.Spawn();
                fragment.transform.parent = parent;
                fragment.transform.localPosition = Vector3.zero;
                result.Add(fragment);
            }
            return result;
        }
    }
}