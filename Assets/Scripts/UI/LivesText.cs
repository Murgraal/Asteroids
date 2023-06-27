using System;
using System.Collections;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Systems.Messaging;
using TMPro;
using UnityEngine;
using Zenject;

public class LivesText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Inject] private GameDataContainer _gameData;
    
    private const string _textPrefix = "Lives: ";

    private void Start()
    {
        _text.text = _textPrefix + _gameData.Data.Lives;
    }

    private void OnEnable()
    {
        NotificationSystem.OnNotify += UpdateLives;
    }

    private void OnDisable()
    {
        NotificationSystem.OnNotify -= UpdateLives;
    }

    private void UpdateLives(NotificationType type)
    {
        if (type == NotificationType.PlayerDied)
        {
            _text.text = _textPrefix + _gameData.Data.Lives;
        }
    }
}
