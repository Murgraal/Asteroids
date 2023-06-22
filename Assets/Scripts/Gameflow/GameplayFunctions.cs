using UnityEngine;

namespace Gameflow
{
    public static class GameplayFunctions
    {
        public static bool IsPositionInsideOfArea(Vector2 position, float maxY, float minY, float maxX, float minX)
        {
            return true;
        }
    
        public static Vector2 GetRandomDirectionFromPosition(Vector2 currentPos)
        {
            return Vector2.left;
        }

        public static Vector2 GetPositionOnOppositeSideOfPlayArea()
        {
            return Vector2.up;
        }
    }
}