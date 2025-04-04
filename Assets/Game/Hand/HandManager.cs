using Game.PlayerAndCards.Cards.PlayerCards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform handPanel;
    [SerializeField] private int minCards = 5;
    [SerializeField] private int maxCards = 8;
    [SerializeField] private int countChangeCards = 4;
    [SerializeField] private int cardFromChest = 5;
    
    private List<PlayerCard> cardsInHand = new List<PlayerCard>();
    private List<CardInDeck> cardInDeck = new List<CardInDeck>();
    private int cardInDeskCount;

    private IInstantiator _instatiator;

    [Inject]
    private void Construct(IInstantiator instatiator)
    {
        _instatiator = instatiator;
    }
    
    void Start()
    {
        cardInDeck = FindObjectsOfType<CardInDeck>(true).ToList();
        cardInDeskCount = 0;
        foreach (CardInDeck card in cardInDeck)
        {
            cardInDeskCount += card.CountOfCard;
        }
        FillHandFirst();
    }

    public void FillHandFirst()
    {
        int cardCount = Random.Range(minCards, maxCards + 1);
        for (int i = 0; i < cardCount; i++)
        {
            int numberOfCard = Random.Range(0, cardInDeck.Count);
            string nameOfCard = cardInDeck[numberOfCard].PlayerCardData.name;
            Sprite imageOfCard = cardInDeck[numberOfCard].PlayerCardData.Sprite;

            if (cardInDeck[numberOfCard].CountOfCard >= 0)
            {
                AddCard(nameOfCard, imageOfCard);
                cardInDeck[numberOfCard].CountOfCard--;
            }
            else
            {
                cardCount++;
            }
        }
        UpdateSpacing();

    }

    public void FillHandAfter()
    {
        int cardCount = maxCards - cardsInHand.Count;

        for (int i = 0; i < cardCount; i++)
        {            
            int numberOfCard = Random.Range(0, cardInDeck.Count);
            string nameOfCard = cardInDeck[numberOfCard].PlayerCardData.name;
            Sprite imageOfCard = cardInDeck[numberOfCard].PlayerCardData.Sprite;

            if (cardInDeck[numberOfCard].CountOfCard > 0)
            {
                AddCard(nameOfCard, imageOfCard);
                cardInDeck[numberOfCard].CountOfCard--;
            }
            else
            {
                cardCount++;
            }
        }
        UpdateSpacing();
    }

    public void RedrawCardsInHand()
    {
        int cardCount = 0;

        if (cardInDeskCount >= countChangeCards && cardsInHand.Count >= countChangeCards)
        {
            cardCount = countChangeCards;
        }
        else if (cardInDeskCount < countChangeCards && cardsInHand.Count >= countChangeCards)
        {
            cardCount = cardInDeskCount;
        }
        else if (cardInDeskCount >= countChangeCards && cardsInHand.Count < countChangeCards)
        {
            cardCount = cardsInHand.Count;
        }
        else if (cardInDeskCount < countChangeCards && cardsInHand.Count < countChangeCards)
        {
            cardCount = Mathf.Min(cardInDeskCount, cardsInHand.Count);
        }

        for (int i = 0; i < cardCount; i++)
        {
            int numberCardInHand = Random.Range(0, cardsInHand.Count);
            ReturnCardInDeck(numberCardInHand);

            int numberOfCard = Random.Range(0, cardInDeck.Count);
            string nameOfCard = cardInDeck[numberOfCard].PlayerCardData.name;
            Sprite imageOfCard = cardInDeck[numberOfCard].PlayerCardData.Sprite;

            if (cardInDeck[numberOfCard].CountOfCard >= 0)
            {
                AddCard(nameOfCard, imageOfCard);
                cardInDeck[numberOfCard].CountOfCard--;
            }
            else
            {
                cardCount++;
            }
        }
        UpdateSpacing();
    }

    public void AddCard(string nameOfCard, Sprite imageOfCard)
    {
        string cardName = nameOfCard+"Card";
        string prefabPath = "PlayerCards/Prefabs/" + cardName;
        PlayerCard card = Resources.Load<PlayerCard>(prefabPath);

        PlayerCard newCard = _instatiator.InstantiatePrefabForComponent<PlayerCard>(card, handPanel);
        
        Image cardImage = newCard.GetComponent<Image>();
        cardImage.sprite = imageOfCard;

        cardsInHand.Add(newCard);        
    }

    public void ReturnCardInDeck(int number)
    {
        PlayerCard card = cardsInHand[number];
        string cardName = card.name.Replace("Card", "").Replace("(Clone)", "").Trim();
        CardInDeck cardData = cardInDeck.Find(c => c.PlayerCardData.name == cardName);
        cardData.CountOfCard++;
        DeleteCardFromHand(card);
    }

    public void DeleteCardFromHand(PlayerCard card)
    {
        cardsInHand.Remove(card);
        Destroy(card.gameObject);
    }    
    
    public void AddCardsInDeck(PlayerCardNames cardNames, int addCount)
    {
        var card = FindCardInDeck(cardNames);
        
        if (card == null) return;
        card.CountOfCard += addCount;
    }

    private CardInDeck FindCardInDeck(PlayerCardNames cardName)
    {
        foreach (var card in cardInDeck)
        {
            if (card.PlayerCardData.Name == cardName) return card; 
        }
        
        Debug.LogError($"Card with name {cardName} was not found in deck!");
        return null;
    }

    public void UpdateSpacing()
    {
        int cardCount = cardsInHand.Count;
        HorizontalLayoutGroup horizontalPanel = handPanel.GetComponent<HorizontalLayoutGroup>();

        if (cardCount == 5) horizontalPanel.spacing = 20f;
        if (cardCount == 6) horizontalPanel.spacing = -20f;
        if (cardCount == 7) horizontalPanel.spacing = -46f;
        if (cardCount == 8) horizontalPanel.spacing = -66f;
        //if (cardCount == 9) horizontalPanel.spacing = -78f;
        //if (cardCount == 10) horizontalPanel.spacing = -89f;
        //if (cardCount == 11) horizontalPanel.spacing = -99f;
        //if (cardCount == 12) horizontalPanel.spacing = -106f;
        //if (cardCount == 13) horizontalPanel.spacing = -112f;
        //if (cardCount == 14) horizontalPanel.spacing = -117f;
        //if (cardCount == 15) horizontalPanel.spacing = -122f;
    }
}
