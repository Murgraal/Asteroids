using Gameflow;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class StartGameButton : MonoBehaviour
    {
        [Inject] private GameStateManager _gameStateManager;
        private Button _button;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_gameStateManager.StartGame);
        }
    }
}
