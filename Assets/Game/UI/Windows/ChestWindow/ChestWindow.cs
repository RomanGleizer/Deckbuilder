using System;
using System.Collections.Generic;
using Game.PlayerAndCards.Cards.PlayerCards;
using UnityEngine;
using Zenject;

public class ChestWindow : Window
{
    [SerializeField] private DeckInChest[] _decks;
    private List<PlayerCardNames> _usedCards = new List<PlayerCardNames>();

    private PlayerCardsContainer _playerCardsContainer;
    
    [Inject]
    private void Construct(PlayerCardsContainer playerCardsContainer)
    {
        _playerCardsContainer = playerCardsContainer;
    }

    private void Awake()
    {
        Subscribe();
    }
    
    public override void ActivateWindow()
    {
        RandomizeDecks();
        base.ActivateWindow();
    }

    public void RandomizeDecks()
    {
        foreach (var deck in _decks)
        {
            PlayerCardData cardData;
            do
            {
                cardData = _playerCardsContainer.GetRandomCardData();
            } while (_usedCards.Contains(cardData.Name));
            
            deck.SetCardData(cardData);
            _usedCards.Add(cardData.Name);
        }
        _usedCards.Clear();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        foreach (var deck in _decks)
        {
            deck.OnDeckChoosed += DeactivateWindow;
        }
    }

    private void Unsubscribe()
    {
        foreach (var deck in _decks)
        {
            deck.OnDeckChoosed -= DeactivateWindow;
        }
    }
}