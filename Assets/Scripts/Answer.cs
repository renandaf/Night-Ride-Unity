using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection.Emit;
using static QuestionData;

public class Answer : MonoBehaviour
{
    private int op;
    private GameManager _gameController;
    public TMP_Text AnswerText;
    private bool _isCorrectAnswer;

    private void Start()
    {
        _gameController = FindObjectOfType<GameManager>();
    }

    public void Initialize(string answer, bool isCorrectAnswer, int op)
    {
        this.op = op;
        AnswerText.text = answer;
        _isCorrectAnswer = isCorrectAnswer;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_gameController.IsSudden)
            {
                _gameController.SetHealth(_gameController.PrevHealth);
                _gameController.IsSudden = false;
            }
            _gameController.IsDouble = false;
            _gameController.IsHalf = false;
            _gameController.RoadController.ChangeSpeed(0);
            _gameController.Player.IsInverted = false;
            switch (op)
            {
                case 0:
                    _gameController.RoadController.ChangeSpeed(1);
                    _gameController.IsDouble = true;
                    break;
                case 1:
                    _gameController.RoadController.ChangeSpeed(2);
                    _gameController.IsHalf = true;
                    break;
                case 2:
                    _gameController.PrevHealth = _gameController.Health;
                    _gameController.SetHealth(1);
                    _gameController.IsSudden = true;
                    break;
                case 3:
                    _gameController.SetHealth(_gameController.maxHealth);
                    break;
                case 4:
                    _gameController.AddCoin(10);
                    _gameController.AddScore(1000);
                    break;
                case 5:
                    _gameController.Player.IsInverted = true;
                    _gameController.IsDouble = true;
                    break;
            }
        }
    }
}
