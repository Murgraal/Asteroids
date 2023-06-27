using System;
using System.Collections;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Systems.Messaging;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Inject] private GameDataContainer _gameData;

    private const string _textPrefix = "Score: ";


    private void Start()
    {
        _text.text = _textPrefix + _gameData.Data.Score;
    }

    private void OnEnable()
    {
        NotificationSystem.OnNotify += UpdateScore;
    }

    private void OnDisable()
    {
        NotificationSystem.OnNotify -= UpdateScore;
    }

    private void UpdateScore(NotificationType type)
    {
        if (type == NotificationType.AsteroidDespawned)
        {
            _text.text = _textPrefix + _gameData.Data.Score;
        }
    }
}
