using System;
using TMPro;
using UnityEngine;

public class BuyButton : CustomButton
{
    [SerializeField] private TextMeshProUGUI _priceText;
     
    public Action OnBuyClicked;
     
    public void Init(int price)
    {
        _priceText.text = price.ToString();
    }

    protected override void OnClick()
    {
        OnBuyClicked?.Invoke();
    }
}