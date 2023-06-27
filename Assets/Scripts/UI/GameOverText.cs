using System.Collections;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Systems.Messaging;
using TMPro;
using UnityEngine;
using Zenject;

public class GameOverText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Inject] private GameDataContainer _gameData;
    
    private const string _gameOverText = "Game Over, you lose!";
    private const string _winText = "You Won";

    private void Start()
    {
        if (_gameData.Data.Lives > 0)
        {
            _text.text = _winText;
        }
        else
        {
            _text.text = _gameOverText;
        }
    }
}
