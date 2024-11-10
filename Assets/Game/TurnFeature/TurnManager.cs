using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class TurnManager
{
    private Button _switchTurnButton;
    private TextMeshProUGUI _turnWarningText;

    private bool _isPlayerTurn = true;
    public bool IsPlayerTurn => _isPlayerTurn;

    public event Action OnPlayerTurn;
    public event Action OnEnemiesTurn;

    public TurnManager(Button switchTurnButton, TextMeshProUGUI turnWarningText)
    {
        _switchTurnButton = switchTurnButton;
        _turnWarningText = turnWarningText;

        UpdateTurnWarningText(_isPlayerTurn);
    }

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        var subscribeHandler = instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);
    }

    private void ChangeTurn()
    {
        if (!_isPlayerTurn) ChangeToPlayerTurn();
        else ChangeToEnemiesTurn();
    }

    private void ChangeToPlayerTurn()
    {
        _isPlayerTurn = true;
        UpdateTurnWarningText(_isPlayerTurn);

        OnPlayerTurn?.Invoke();
    }

    private void ChangeToEnemiesTurn()
    {
        _isPlayerTurn = false;
        UpdateTurnWarningText(_isPlayerTurn);

        OnEnemiesTurn?.Invoke();
    }

    private void UpdateTurnWarningText(bool isPlayerTurn)
    {
        if (isPlayerTurn) _turnWarningText.text = "Ход игрока";
        else _turnWarningText.text = "Ход врагов";
    }

    private void Subscribe()
    {
        _switchTurnButton.onClick.AddListener(ChangeTurn);
    }

    private void Unsubscribe()
    {
        _switchTurnButton.onClick.RemoveListener(ChangeTurn);
    }
}
