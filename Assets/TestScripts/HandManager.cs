using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;        // Префаб карты
    public Transform handPanel;          // Панель руки (с Horizontal Layout Group)
    public int minCards = 5;             // Минимальное количество карт
    public int maxCards = 8;             // Максимальное количество карт
    public Button drawButton;            // Кнопка для отображения карт
    public float cardWidth = 183f;       // Ширина одной карты
    public float handWidth = 1003f;       // Фиксированная ширина панели руки

    private List<GameObject> cardsInHand = new List<GameObject>();

    void Start()
    {
        // Привязываем функцию к кнопке
        drawButton.onClick.AddListener(DrawCardsInHand);
        
    }

    // Функция для отображения карт в руке
    void DrawCardsInHand()
    {
        // Очищаем старые карты (если есть)
        foreach (var card in cardsInHand)
        {
            Destroy(card);
        }
        cardsInHand.Clear();

        HorizontalLayoutGroup horizontalPanel = handPanel.GetComponent<HorizontalLayoutGroup>();

        // Генерация случайного числа карт в руке
        int cardCount = Random.Range(minCards, maxCards + 1);

        if (cardCount == 5) horizontalPanel.spacing = 20f;
        if (cardCount == 6) horizontalPanel.spacing = -20f;
        if (cardCount == 7) horizontalPanel.spacing = -46f;
        if (cardCount == 8) horizontalPanel.spacing = -66f;


        // Добавляем карты в панель "руки"
        for (int i = 0; i < cardCount; i++)
        {
            // Создаем копию префаба карты
            GameObject newCard = Instantiate(cardPrefab, handPanel);       
            Image cardImage = newCard.GetComponent<Image>();
            cardImage.color = Random.ColorHSV();

            // Добавляем карту в список для управления
            cardsInHand.Add(newCard);
        }

    }
}
