using System;
using System.Collections;
using Game.PlayerAndCards.Cards.PlayerCards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Zenject;

public class DeckInChest : CustomButton
{
    [SerializeField] private Image _cardImage;
    [SerializeField] private TextMeshProUGUI _countText;

    private PlayerCardData _playerCardData;

    public event Action OnDeckChoosed;
    
    public void SetCardData(PlayerCardData cardData)
    {
        _cardImage.sprite = cardData.Sprite;
        _countText.text = "X" + cardData.CountInChest;
        
        _playerCardData = cardData;
    }
    
    protected override void OnClick()
    {
        Debug.Log($"Add card {_playerCardData.Name} count: {_playerCardData.CountInChest}");
        SaveService.SaveData.Deck.AddCardInDeck(_playerCardData.Name, _playerCardData.CountInChest);
        SaveService.Save();
        OnDeckChoosed?.Invoke();
    }
}