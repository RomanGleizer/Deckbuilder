using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;        // ������ �����
    public Transform handPanel;          // ������ ���� (� Horizontal Layout Group)
    public int minCards = 5;             // ����������� ���������� ����
    public int maxCards = 8;             // ������������ ���������� ����
    public Button drawButton;            // ������ ��� ����������� ����
    public float cardWidth = 183f;       // ������ ����� �����
    public float handWidth = 1003f;       // ������������� ������ ������ ����

    private List<GameObject> cardsInHand = new List<GameObject>();

    void Start()
    {
        // ����������� ������� � ������
        drawButton.onClick.AddListener(DrawCardsInHand);
        
    }

    // ������� ��� ����������� ���� � ����
    void DrawCardsInHand()
    {
        // ������� ������ ����� (���� ����)
        foreach (var card in cardsInHand)
        {
            Destroy(card);
        }
        cardsInHand.Clear();

        HorizontalLayoutGroup horizontalPanel = handPanel.GetComponent<HorizontalLayoutGroup>();

        // ��������� ���������� ����� ���� � ����
        int cardCount = Random.Range(minCards, maxCards + 1);

        if (cardCount == 5) horizontalPanel.spacing = 20f;
        if (cardCount == 6) horizontalPanel.spacing = -20f;
        if (cardCount == 7) horizontalPanel.spacing = -46f;
        if (cardCount == 8) horizontalPanel.spacing = -66f;


        // ��������� ����� � ������ "����"
        for (int i = 0; i < cardCount; i++)
        {
            // ������� ����� ������� �����
            GameObject newCard = Instantiate(cardPrefab, handPanel);       
            Image cardImage = newCard.GetComponent<Image>();
            cardImage.color = Random.ColorHSV();

            // ��������� ����� � ������ ��� ����������
            cardsInHand.Add(newCard);
        }

    }
}
