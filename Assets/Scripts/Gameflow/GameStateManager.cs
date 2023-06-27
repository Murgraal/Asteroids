using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameflow
{
    public class GameStateManager : MonoBehaviour
    {
        public void GoToMenus()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Gameplay");
        }

        public void GoToScoreScreen()
        {
            SceneManager.LoadScene("ScoreScreen");
        }
    }
}