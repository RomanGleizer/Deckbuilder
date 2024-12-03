using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenCardInfo : CustomButton
{
    [SerializeField] GameObject cardInfoWindow;
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardInfo;
    [SerializeField] int cardNumber;
    protected override void OnClick()
    {
        cardInfoWindow.SetActive(true);
        //добавить сюда Sctiptable object
        cardName.text = "Карточка номер" + cardNumber.ToString();
        cardInfo.text = "Информация о карточке номер" + cardNumber.ToString();
    }
}
