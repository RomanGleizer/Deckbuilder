using System;
using Game.PlayerAndCards.Cards.PlayerCards;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInDeck : MonoBehaviour
{
    [SerializeField] private PlayerCardData _playerCardData;
    [SerializeField] private int _countOfCard;
    [SerializeField] private TextMeshProUGUI _cardCountText;
    [SerializeField] private Image _cardImage;


    public PlayerCardData PlayerCardData => _playerCardData;
    public int CountOfCard
    {
        get
        {
            return _countOfCard;    
        }
        set
        {
            _countOfCard = value;
            SaveService.SaveData.Deck.UpdateCardInDeck(_playerCardData.Name, value);
            SaveService.Save();
        }
    }

    private void Start()
    {
        _countOfCard = SaveService.SaveData.Deck.GetCardCounts(_playerCardData.Name);
        SaveService.SaveData.Deck.OnCardUpdated += UpdateCard;
        
        _cardImage.sprite = _playerCardData.Sprite;
        _cardCountText.text = "x" + _countOfCard;
    }

    private void OnEnable()
    {
        _cardCountText.text = "x" + _countOfCard;
    }

    public void UpdateCard(PlayerCardInDeckData cardInDeck)
    {
        if (cardInDeck.PlayerCardNames != _playerCardData.Name.ToString()) return;
        _countOfCard = cardInDeck.Counts;
    }

    private void OnDestroy()
    {
        SaveService.SaveData.Deck.OnCardUpdated -= UpdateCard;
    }
}

