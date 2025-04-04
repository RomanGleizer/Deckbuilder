using Game.PlayerAndCards.Cards.PlayerCards;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OpenCardInfo : CustomButton
{
    private WindowActivator _windowActivator;
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardInfo;

    [Inject]
    private void Construct(WindowActivator windowActivator)
    {
        _windowActivator = windowActivator;
    }
    
    protected override void OnClick()
    {
        CardInDeck cardData = GetComponent<CardInDeck>();

        _windowActivator.ActivateWindow(WindowType.CardInfo);
        _cardName.text = cardData.PlayerCardData.NameInRussian;
        _cardInfo.text = cardData.PlayerCardData.Description;        
    }
}
