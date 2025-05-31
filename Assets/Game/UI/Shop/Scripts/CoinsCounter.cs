using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _coinsText;

    private int _currentCoins = 0;
    public int Coins => _currentCoins;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentCoins = SaveService.SaveData.Coins;
        UpdateView();
    }

    public void AddCoins(int amount)
    {
        _currentCoins += amount;
        
        SaveService.SaveData.Coins = _currentCoins;
        SaveService.Save();
        
        UpdateView();
    }

    public void RemoveCoins(int amount)
    { 
        _currentCoins = Mathf.Clamp(_currentCoins - amount, 0, _currentCoins);
        
        SaveService.SaveData.Coins = _currentCoins;
        SaveService.Save();
        
        UpdateView();
    }

    private void UpdateView()
    {
        _coinsText.text = _currentCoins.ToString();
    }
}
