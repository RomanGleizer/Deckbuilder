using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;        // ������ �����
    public Transform handPanel;          // ������ ���� (� Horizontal Layout Group)
    public int minCards = 3;             // ����������� ���������� ����
    public int maxCards = 6;             // ������������ ���������� ����
    public Button drawButton;            // ������ ��� ����������� ����
    public float cardWidth = 120f;       // ������ ����� �����
    public float spacing = 10f;          // ������������� ���������� ����� �������
    public float handWidth = 800f;       // ������������� ������ ������ ����

    private List<GameObject> cardsInHand = new List<GameObject>();

    void Start()
    {
        // ����������� ������� � ������
        drawButton.onClick.AddListener(DrawCardsInHand);

        // ������������� ������������� ������ ������ ����
        RectTransform handRect = handPanel.GetComponent<RectTransform>();
        handRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, handWidth);
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

        // ��������� ���������� ����� ���� � ����
        int cardCount = Random.Range(minCards, maxCards + 1);

        // ������������ ����� ������ ���� ���� � ������� ����� ����
        float totalCardsWidth = cardCount * cardWidth + (cardCount - 1) * spacing;
        print("total" + totalCardsWidth);

        // ������������ ������� ����� � ������ (���� ���� ������ ��� ������ ������ ����)
        float remainingSpace = handWidth - totalCardsWidth;
        print(remainingSpace);
        float padding = Mathf.Max(remainingSpace / 2, 0);
        print("padding" + padding);

        // �������� ��������� Horizontal Layout Group � ����������� �������
        HorizontalLayoutGroup layoutGroup = handPanel.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.padding.left = Mathf.RoundToInt(padding);
        layoutGroup.padding.right = Mathf.RoundToInt(padding);
        layoutGroup.spacing = spacing; // ������ ������������� ���������� ����� �������

        // ��������� ����� � ������ "����"
        for (int i = 0; i < cardCount; i++)
        {
            // ������� ����� ������� �����
            GameObject newCard = Instantiate(cardPrefab, handPanel);

            // ��������� ����� � ������ ��� ����������
            cardsInHand.Add(newCard);
        }

        // ��������� ������������ ����
        LayoutRebuilder.ForceRebuildLayoutImmediate(handPanel.GetComponent<RectTransform>());
    }
}
