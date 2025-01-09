using Game.PlayerAndCards.Cards.PlayerCards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public Transform handPanel;
    public int minCards = 5;
    public int maxCards = 8;
    public float cardWidth = 183f;
    public float handWidth = 1003f;

    private List<GameObject> cardsInHand = new List<GameObject>();
    private List<CardInDesk> cardInDeck;

    void Start()
    {
        cardInDeck = FindObjectsOfType<CardInDesk>(true).ToList();
    }

    public void DrawCardsInHand()
    {
        ClearHand();

        HorizontalLayoutGroup horizontalPanel = handPanel.GetComponent<HorizontalLayoutGroup>();

        int cardCount = Random.Range(minCards, maxCards + 1);

        if (cardCount == 5) horizontalPanel.spacing = 20f;
        if (cardCount == 6) horizontalPanel.spacing = -20f;
        if (cardCount == 7) horizontalPanel.spacing = -46f;
        if (cardCount == 8) horizontalPanel.spacing = -66f;


        for (int i = 0; i < cardCount; i++)
        {
            int numberOfCard = Random.Range(0, cardInDeck.Count);
            string nameOfCard = cardInDeck[numberOfCard].PlayerCardData.name;
            Sprite imageOfCard = cardInDeck[numberOfCard].PlayerCardData.Sprite;
            print(nameOfCard);

            if (cardInDeck[numberOfCard].CountOfCard >= 0)
            {
                AddCard(nameOfCard, imageOfCard);
                cardInDeck[numberOfCard].CountOfCard--;
            }            
        }
    }

    public void AddCard(string nameOfCard, Sprite imageOfCard)
    {
        string cardName = nameOfCard+"Card";
        string prefabPath = "PlayerCards/Prefabs/" + cardName;
        GameObject card = Resources.Load<GameObject>(prefabPath);

        GameObject newCard = Instantiate(card, handPanel);
        Image cardImage = newCard.GetComponent<Image>();
        cardImage.sprite = imageOfCard;

        cardsInHand.Add(newCard);
    }

    public void DeleteCard(GameObject card)
    {
        
    }

    public void ClearHand()
    {
        foreach (GameObject card in cardsInHand)
        {
            Destroy(card);
        }
        cardsInHand.Clear();
    }
}
