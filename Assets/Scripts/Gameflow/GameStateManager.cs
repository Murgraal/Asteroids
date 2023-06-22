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
    
        private void Update()
        {
            DebugSceneChange();
        }

        private void DebugSceneChange()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                GoToMenus();
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                GoToScoreScreen();
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                StartGame();
            }
        }
    }
}