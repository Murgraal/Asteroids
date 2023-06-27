using UnityEngine;

namespace Gameflow
{
    public static class GameplayFunctions
    {
        public static Vector3 GetRandomPositionOnScreen()
        {
            var screenPos = new Vector3(Random.Range(0f, Screen.height), Random.Range(0, Screen.height), 0);
            var spawnPos = Camera.main.ScreenToWorldPoint(screenPos);
            spawnPos.z = -10;
            return spawnPos;
        }


        public static void LaunchInRandomDirection(Rigidbody2D rigid, float flySpeed)
        {
            var delta = GetRandomPositionOnScreen() - (Vector3)rigid.position;
            rigid.AddForce(delta.normalized * flySpeed,ForceMode2D.Impulse);
        }
    }
}