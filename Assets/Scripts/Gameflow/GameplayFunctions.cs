using UnityEngine;

namespace Gameflow
{
    public static class GameplayFunctions
    {
        public static Vector2 GetRandomPositionOnScreen()
        {
            var screenPos = new Vector3(Random.Range(0f, Screen.height), Random.Range(0, Screen.height), 0);
            var spawnPos = Camera.current.ScreenToWorldPoint(screenPos);
            return spawnPos;
        }
    }
}