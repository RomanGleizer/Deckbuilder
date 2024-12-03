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
        //�������� ���� Sctiptable object
        cardName.text = "�������� �����" + cardNumber.ToString();
        cardInfo.text = "���������� � �������� �����" + cardNumber.ToString();
    }
}
