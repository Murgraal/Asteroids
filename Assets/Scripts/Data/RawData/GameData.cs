using System;
using UnityEngine;

namespace Data.RawData
{
    [Serializable]
    public class GameData
    {
        public Vector2 PlayerPosition;
        public int Score = 0;
        public int Lives = 5;
        public int Level = 0;
    }
}