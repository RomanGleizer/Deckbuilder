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
    [SerializeField] private int maxCards = 15;
    
    private List<PlayerCard> cardsInHand;
    private List<CardInDesk> cardInDeck;
    private List<int> numberOfCardsInHand;

    private IInstantiator _instatiator;

    [Inject]
    private void Construct(IInstantiator instatiator)
    {
        _instatiator = instatiator;
    }
    
    void Start()
    {
        cardInDeck = FindObjectsOfType<CardInDesk>(true).ToList();
    }

    public void RedrawCardsInHand()
    {
        ClearHand();        

        int cardCount = Random.Range(minCards, maxCards + 1);   

        for (int i = 0; i < cardCount; i++)
        {
            int numberOfCard = Random.Range(0, cardInDeck.Count);
            string nameOfCard = cardInDeck[numberOfCard].PlayerCardData.name;
            Sprite imageOfCard = cardInDeck[numberOfCard].PlayerCardData.Sprite;

            if (cardInDeck[numberOfCard].CountOfCard >= 0)
            {
                AddCard(nameOfCard, imageOfCard);
                numberOfCardsInHand.Add(numberOfCard);
                cardInDeck[numberOfCard].CountOfCard--;
            }
            else
            {
                cardCount++;
            }
        }
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

    public void ReturnCardsInDeck()
    {
        foreach (int number in numberOfCardsInHand)
        {
            cardInDeck[number].CountOfCard++;
        }
        ClearHand();
    }

    public void DeleteCard(PlayerCard card)
    {
        Destroy(card.gameObject);
    }

    public void ClearHand()
    {
        foreach (PlayerCard card in cardsInHand)
        {
            DeleteCard(card);
        }
        cardsInHand.Clear();
        numberOfCardsInHand.Clear();
    }

    public void UpdateSpacing()
    {
        int cardCount = cardsInHand.Count;
        HorizontalLayoutGroup horizontalPanel = handPanel.GetComponent<HorizontalLayoutGroup>();

        if (cardCount == 5) horizontalPanel.spacing = 20f;
        if (cardCount == 6) horizontalPanel.spacing = -20f;
        if (cardCount == 7) horizontalPanel.spacing = -46f;
        if (cardCount == 8) horizontalPanel.spacing = -66f;
        if (cardCount == 9) horizontalPanel.spacing = -78f;
        if (cardCount == 10) horizontalPanel.spacing = -89f;
        if (cardCount == 11) horizontalPanel.spacing = -99f;
        if (cardCount == 12) horizontalPanel.spacing = -106f;
        if (cardCount == 13) horizontalPanel.spacing = -112f;
        if (cardCount == 14) horizontalPanel.spacing = -117f;
        if (cardCount == 15) horizontalPanel.spacing = -122f;
    }
}
