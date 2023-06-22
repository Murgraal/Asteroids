using System.Collections.Generic;
using Gameflow;
using UnityEngine;

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