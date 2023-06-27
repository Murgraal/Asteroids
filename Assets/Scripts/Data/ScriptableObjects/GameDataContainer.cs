using Data.RawData;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/GameDataContainer")]
    public class GameDataContainer : ScriptableObject
    {
        public GameData Data;

        public void Reset()
        {
            Data = new GameData();
        }
    }
}