using System;
using System.Collections.Generic;
using Game.PlayerAndCards.Cards.PlayerCards;
using UnityEngine;

[System.Serializable]
public class Deck
{
    public List<PlayerCardInDeckData> PlayerCards = InitList();

    public Action<PlayerCardInDeckData> OnCardUpdated;
    
    private static List<PlayerCardInDeckData> InitList()
    {
        var list = new List<PlayerCardInDeckData>();

        for (int i = 0; i < Enum.GetNames(typeof(PlayerCardNames)).Length; i++)
        {
            list.Add(new PlayerCardInDeckData(((PlayerCardNames)i).ToString(), 3));
        }
        return list;
    }
    
    public void AddCardInDeck(PlayerCardNames cardNames, int count)
    {
        GetCardInDeck(cardNames).AddCounts(count);
        OnCardUpdated?.Invoke(GetCardInDeck(cardNames));
    }
    
    public void RemoveCardInDeck(PlayerCardNames cardNames, int count)
    {
        GetCardInDeck(cardNames).RemoveCounts(count);
        OnCardUpdated?.Invoke(GetCardInDeck(cardNames));
    }
    
    public void UpdateCardInDeck(PlayerCardNames cardNames, int count)
    {
        if (GetCardInDeck(cardNames).Counts == count) return;
        GetCardInDeck(cardNames).UpdateCounts(count);
        OnCardUpdated?.Invoke(GetCardInDeck(cardNames));
    }
    
    public int GetCardCounts(PlayerCardNames cardNames)
    {
        return GetCardInDeck(cardNames).Counts;
    }

    public PlayerCardInDeckData GetCardInDeck(PlayerCardNames cardNames)
    {
        foreach (var card in PlayerCards)
        {
            if (card.PlayerCardNames == cardNames.ToString()) return card;
        }   
        
        throw new Exception("No card in deck");
    }
}

[System.Serializable]
public class PlayerCardInDeckData
{
    public string PlayerCardNames;
    public int Counts;
        
    public PlayerCardInDeckData(string playerCardNames, int counts)
    {
        PlayerCardNames = playerCardNames;
        Counts = counts;
    }

    public void AddCounts(int counts)
    {
        Counts += counts;
    }

    public void RemoveCounts(int counts)
    {
        Counts = Mathf.Clamp(Counts - counts, 0, Counts);
    }
    
    public void UpdateCounts(int counts)
    {
        Counts = counts;
    }
}