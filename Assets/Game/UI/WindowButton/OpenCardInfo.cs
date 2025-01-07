using Game.PlayerAndCards.Cards.PlayerCards;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenCardInfo : CustomButton
{
    [SerializeField] private GameObject _cardInfoWindow;
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardInfo;
    protected override void OnClick()
    {
        CardInDesk cardData = GetComponent<CardInDesk>();

        _cardInfoWindow.SetActive(true);
        _cardName.text = cardData.PlayerCardData.NameInRussian;
        _cardInfo.text = cardData.PlayerCardData.Description;        
    }
}
