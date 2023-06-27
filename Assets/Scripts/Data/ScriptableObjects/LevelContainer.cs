using System.Collections.Generic;
using Data.RawData;
using UnityEngine;

namespace Gameflow
{
    [CreateAssetMenu(menuName = "Data/LevelContainer")]
    public class LevelContainer : ScriptableObject
    {
        public List<Level> Levels;
    }
}