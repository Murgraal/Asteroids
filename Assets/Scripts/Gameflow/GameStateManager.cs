using System;
using Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gameflow
{
    public class GameStateManager : MonoBehaviour
    {
        [Inject] private GameDataContainer _gameData;
        
        public void GoToMenus()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void StartGame()
        {
            _gameData.Reset();
            SceneManager.LoadScene("Gameplay");
        }

        public void GoToScoreScreen()
        {
            SceneManager.LoadScene("ScoreScreen");
        }
    }
}