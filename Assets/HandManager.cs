using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;        // Префаб карты
    public Transform handPanel;          // Панель руки (с Horizontal Layout Group)
    public int minCards = 3;             // Минимальное количество карт
    public int maxCards = 6;             // Максимальное количество карт
    public Button drawButton;            // Кнопка для отображения карт
    public float cardWidth = 120f;       // Ширина одной карты
    public float spacing = 10f;          // Фиксированное расстояние между картами
    public float handWidth = 800f;       // Фиксированная ширина панели руки

    private List<GameObject> cardsInHand = new List<GameObject>();

    void Start()
    {
        // Привязываем функцию к кнопке
        drawButton.onClick.AddListener(DrawCardsInHand);

        // Устанавливаем фиксированную ширину панели руки
        RectTransform handRect = handPanel.GetComponent<RectTransform>();
        handRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, handWidth);
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

        // Генерация случайного числа карт в руке
        int cardCount = Random.Range(minCards, maxCards + 1);

        // Рассчитываем общую ширину всех карт и отступы между ними
        float totalCardsWidth = cardCount * cardWidth + (cardCount - 1) * spacing;
        print("total" + totalCardsWidth);

        // Рассчитываем отступы слева и справа (если карт меньше чем ширина панели руки)
        float remainingSpace = handWidth - totalCardsWidth;
        print(remainingSpace);
        float padding = Mathf.Max(remainingSpace / 2, 0);
        print("padding" + padding);

        // Получаем компонент Horizontal Layout Group и настраиваем паддинг
        HorizontalLayoutGroup layoutGroup = handPanel.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.padding.left = Mathf.RoundToInt(padding);
        layoutGroup.padding.right = Mathf.RoundToInt(padding);
        layoutGroup.spacing = spacing; // Задаем фиксированное расстояние между картами

        // Добавляем карты в панель "руки"
        for (int i = 0; i < cardCount; i++)
        {
            // Создаем копию префаба карты
            GameObject newCard = Instantiate(cardPrefab, handPanel);

            // Добавляем карту в список для управления
            cardsInHand.Add(newCard);
        }

        // Обновляем расположение карт
        LayoutRebuilder.ForceRebuildLayoutImmediate(handPanel.GetComponent<RectTransform>());
    }
}
