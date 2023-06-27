using System;
using Entities.General;
using UnityEngine;

namespace Data.RawData
{
    [Serializable]
    public class GameData
    {
        public Vector3 PlayerPosition = Vector3.zero;
        public PlayField PlayFieldBoundaries;
        public int Score = 0;
        public int Lives = 5;
        public int Level = 0;
    }
}